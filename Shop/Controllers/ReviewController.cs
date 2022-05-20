

namespace Shop.Controllers;
public class ReviewController : Controller
{
    private const int DefaultPageSize = 25;

    private readonly IRepository<Review> _reviewRepository;
    private readonly shopContext _context;
    private readonly IWorkContext _workContext;
    public ReviewController(IRepository<Review> reviewRepository, shopContext context, IWorkContext workContext)
    {
        _reviewRepository = reviewRepository;
        _context = context;
        _workContext = workContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(ReviewForm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _workContext.GetCurrentUser();
            model.ReviewerName = user.FullName; // Otherwise ReviewerName is null
            var review = new Review
            {
                Rating = model.Rating,
                Title = model.Title,
                Comment = model.Comment,
                ReviewerName = user.FullName,
                EntityId = model.EntityId,
                EntityTypeId = model.EntityTypeId,
                UserId = user.Id,
            };

            _reviewRepository.Add(review);
            _reviewRepository.SaveChanges();

            return PartialView("_ReviewFormSuccess", model);
        }
        return PartialView("_ReviewForm", model);
    }

    private IQueryable<ReplyListItemDto> List()
    {
        var items = from rv in _context.Set<Review>()
                    join e in _context.Set<Entity>()
                    on new { key1 = rv.EntityId, key2 = rv.EntityTypeId } equals new { key1 = e.EntityId, key2 = e.EntityTypeId }
                    join rp in _context.Set<Reply>()
                    on rv.Id equals rp.ReviewId
                    select new ReplyListItemDto
                    {
                        Id = rp.Id,
                        ReplierName = rp.ReplierName,
                        Comment = rp.Comment,
                        Status = rp.Status,
                        CreatedOn = rp.CreatedOn,
                        ReviewTitle = rv.Title,
                        EntityName = e.Name,
                        EntitySlug = e.Slug
                    };

        return items;
    }
    [HttpGet]
    public async Task<IActionResult> List(long entityId, string entityTypeId, int? pageNumber, int? pageSize)
    {
        var entity = List().FirstOrDefault();
        if (entity == null)
        {
            return Redirect("~/Error/FindNotFound");
        }

        var itemsPerPage = pageSize.HasValue ? pageSize.Value : DefaultPageSize;
        var currentPageNum = pageNumber.HasValue ? pageNumber.Value : 1;
        var offset = (itemsPerPage * currentPageNum) - itemsPerPage;

        var model = new ReviewVm();

        model.EntityName = entity.EntityName;
        model.EntitySlug = entity.EntitySlug;

        var query = _reviewRepository.Query()
            .Where(x => (x.EntityId == entityId) && (x.EntityTypeId == entityTypeId) && (x.Status == ReviewStatus.Approved))
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new ReviewItem
            {
                Id = x.Id,
                Title = x.Title,
                Rating = x.Rating,
                Comment = x.Comment,
                ReviewerName = x.ReviewerName,
                CreatedOn = x.CreatedOn,
                Replies = x.Replies
                    .Where(r => r.Status == ReplyStatus.Approved)
                    .OrderByDescending(r => r.CreatedOn)
                    .Select(r => new Reply
                    {
                        Comment = r.Comment,
                        ReplierName = r.ReplierName,
                        CreatedOn = r.CreatedOn
                    })
                    .ToList()
            });

        model.Items.Data = await query
            .Skip(offset)
            .Take(itemsPerPage)
            .ToListAsync();

        model.Items.PageNumber = currentPageNum;
        model.Items.PageSize = itemsPerPage;
        model.Items.TotalItems = await query.CountAsync();

        var allItems = await query.ToListAsync();

        model.ReviewsCount = allItems.Count;
        model.Rating1Count = allItems.Count(x => x.Rating == 1);
        model.Rating2Count = allItems.Count(x => x.Rating == 2);
        model.Rating3Count = allItems.Count(x => x.Rating == 3);
        model.Rating4Count = allItems.Count(x => x.Rating == 4);
        model.Rating5Count = allItems.Count(x => x.Rating == 5);

        model.EntityId = entityId;
        model.EntityTypeId = entityTypeId;

        return View(model);
    }
}


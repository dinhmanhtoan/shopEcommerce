using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ReviewController : Controller
{
    private readonly shopContext _context;
    private readonly IMediator _mediator;
    private readonly IRepository<Review> _reviewRepository;

    public ReviewController(shopContext context, IMediator mediator, IRepository<Review> reviewRepository)
    {
        _context = context;
        _mediator = mediator;
        _reviewRepository = reviewRepository;
    }
    private IQueryable<ReviewListItemDto> List()
    {
        var items = from rv in _context.Set<Review>()
                    join e in _context.Set<Entity>()
                    on new { key1 = rv.EntityId, key2 = rv.EntityTypeId } equals new { key1 = e.EntityId, key2 = e.EntityTypeId }
                    select new ReviewListItemDto
                    {
                        EntityTypeId = rv.EntityTypeId,
                        Id = rv.Id,
                        ReviewerName = rv.ReviewerName,
                        Rating = rv.Rating,
                        Title = rv.Title,
                        Comment = rv.Comment,
                        Status = rv.Status,
                        CreatedOn = rv.CreatedOn,
                        EntityName = e.Name,
                        EntitySlug = e.Slug
                    };

        return items;
    }
    [HttpPost("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus(long id, int statusId)
    {
        var review = _reviewRepository.Query().FirstOrDefault(x => x.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        if (Enum.IsDefined(typeof(ReviewStatus), statusId))
        {
            review.Status = (ReviewStatus)statusId;
            _context.SaveChanges();

            var rattings = _reviewRepository.Query()
                .Where(x => x.EntityId == review.EntityId && x.EntityTypeId == review.EntityTypeId && x.Status == ReviewStatus.Approved);

            var reviewSummary = new ReviewSummaryChanged
            {
                EntityId = review.EntityId,
                EntityTypeId = review.EntityTypeId,
                ReviewsCount = rattings.Count()
            };
            if (reviewSummary.ReviewsCount == 0)
            {
                reviewSummary.RatingAverage = null;
            }
            else
            {
                var grouped = rattings.GroupBy(x => x.Rating).Select(x => new { Rating = x.Key, Count = x.Count() }).ToList();
                reviewSummary.RatingAverage = grouped.Select(x => x.Rating * x.Count).Sum() / (double)reviewSummary.ReviewsCount;
            }

            await _mediator.Publish(reviewSummary);
            await _context.SaveChangesAsync();
            return Accepted();
        }
        return BadRequest(new { Error = "unsupported order status" });
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> query()
    {
    
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var Review = List();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                Review = Review.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                Review = Review.Where(m => m.Title.Contains(searchValue));
            }
            recordsTotal = await Review.CountAsync();
            var data = await Review.Skip(skip).Take(pageSize).ToListAsync();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
    }
    [HttpGet]
    public  IActionResult Get()
    {
        var Review = List().Take(5);

        return Json(Review);
    }
}


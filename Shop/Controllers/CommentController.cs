namespace Shop.Controllers;

[Route("comments")]
public class CommentController : Controller
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IWorkContext _workContext;


    public CommentController(IWorkContext workContext, IRepository<Comment> commentRepository)
    {
        _workContext = workContext;
        _commentRepository = commentRepository;

    }
    [HttpGet]
    public async Task<IActionResult> Get(long entityId, string entityTypeId, string search, int pageSize, int pageNumber)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var offset = (pageSize * pageNumber) - pageSize;
        var query = _commentRepository.Query().Where(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId && x.Parent == null);
        if (!User.IsInRole("admin"))
        {
            query = query.Where(x => x.UserId == currentUser.Id || x.Status == CommentStatus.Approved);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.CommenterName.Contains(search));
        }
        var totalItems = await query.CountAsync();
        var items = await query.OrderByDescending(x => x.CreatedOn).Select(x => new CommentItem
        {
            Id = x.Id,
            CommentText = x.CommentText,
            CommenterName = x.CommenterName,
            CreatedOn = x.CreatedOn,
            Status = x.Status.ToString(),
            Replies = x.Replies
                        .Where(r => r.Status == CommentStatus.Approved)
                        .OrderBy(r => r.CreatedOn)
                        .Select(r => new CommentItem()
                        {
                            Id = r.Id,
                            CommentText = r.CommentText,
                            CommenterName = r.CommenterName,
                            CreatedOn = r.CreatedOn,
                            Status = x.Status.ToString()
                        })
        })
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { TotalItems = totalItems, Items = items });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(CommentForm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _workContext.GetCurrentUser();
            var comment = new Comment
            {
                ParentId = model.ParentId,
                CommentText = model.CommentText,
                CommenterName = user.FullName,
                Status = CommentStatus.Approved,
                EntityId = model.EntityId,
                EntityTypeId = model.EntityTypeId,
                UserId = user.Id
            };

            if (!User.IsInRole("admin"))
            {
                var isCommentsRequireApproval = true;
                comment.Status = isCommentsRequireApproval ? CommentStatus.Pending : CommentStatus.Approved;
            }

            _commentRepository.Add(comment);
            await _commentRepository.SaveChangesAsync();
            var commentItem = new CommentItem
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                CommenterName = comment.CommenterName,
                CreatedOn = comment.CreatedOn,
                Status = comment.Status.ToString()
            };

            return Ok(commentItem);
        }

        return BadRequest(ModelState);
    }

}


using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ReplyController : Controller
{
    private readonly shopContext _context;
    private readonly IMediator _mediator;
    private readonly IRepository<Reply> _replyRepository;

    public ReplyController(shopContext context, IMediator mediator, IRepository<Reply> replyRepository)
    {
        _context = context;
        _mediator = mediator;
        _replyRepository = replyRepository;
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
        var Reply = List();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Reply = Reply.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Reply = Reply.Where(m => m.ReviewTitle.Contains(searchValue));
        }
        recordsTotal = await Reply.CountAsync();
        var data = await Reply.Skip(skip).Take(pageSize).ToListAsync();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpPost("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus(long id,int statusId)
    {
        var reply = _replyRepository.Query().FirstOrDefault(x => x.Id == id);

        if (reply == null)
        {
            return NotFound();
        }

        if (Enum.IsDefined(typeof(ReplyStatus), statusId))
        {
            reply.Status = (ReplyStatus)statusId;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        return BadRequest(new { Error = "unsupported reply status" });
    }
    [HttpGet]
    public IActionResult Get()
    {
        var reply = List().Take(5);

        return Json(reply);
    }
}


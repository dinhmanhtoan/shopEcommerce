using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class CommentController : Controller
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly shopContext _context;
    public CommentController(IRepository<Comment> commentRepository, shopContext context)
    {
        _commentRepository = commentRepository;
        _context = context;
    }
    [HttpPost("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus(long id,int statusId)
    {
        var comment = _commentRepository.Query().FirstOrDefault(x => x.Id == id);
        if (comment == null)
        {
            return NotFound();
        }

        if (Enum.IsDefined(typeof(CommentStatus), statusId))
        {
            comment.Status = (CommentStatus)statusId;
            _commentRepository.SaveChanges();

            await _commentRepository.SaveChangesAsync();
            return Accepted();
        }

        return BadRequest(new { Error = "unsupported order status" });
    }
    private IQueryable<CommentListItemDto> List()
    {
        var items = from cm in _context.Set<Comment>()
                    join e in _context.Set<Entity>()
                    on new { key1 = cm.EntityId, key2 = cm.EntityTypeId } equals new { key1 = e.EntityId, key2 = e.EntityTypeId }
                    select new CommentListItemDto
                    {
                        Id = cm.Id,
                        EntityTypeId = cm.EntityTypeId,
                        EntityId = cm.EntityId,
                        EntityName = e.Name,
                        EntitySlug = e.Slug,
                        UserId = cm.UserId,
                        ParentId = cm.ParentId,
                        CommentText = cm.CommentText,
                        CommenterName = cm.CommenterName,
                        Status = cm.Status,
                        CreatedOn = cm.CreatedOn

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
        var comment = List();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            comment = comment.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            comment = comment.Where(m => m.EntityName.Contains(searchValue));
        }
        recordsTotal = await comment.CountAsync();
        var data = await comment.Skip(skip).Take(pageSize).ToListAsync();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
}



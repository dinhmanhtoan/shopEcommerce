namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class KeywordController : Controller
{
    private readonly IRepository<Query> _queryRepository;
    public KeywordController(IRepository<Query> queryRepository)
    {
        _queryRepository = queryRepository;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var model = _queryRepository.Query()
        .GroupBy(x => x.QueryText)
        .OrderByDescending(g => g.Count())
        .Select(g => new { Keyword = g.Key, Count = g.Count() })
        .Take(5);

        return Json(model);
    }
}

namespace Shop.Controllers;
public class PagesController : Controller
{
    private readonly IRepository<Pages> _pageRepository;
    public PagesController(IRepository<Pages> pageRepository)
    {
        _pageRepository = pageRepository;
    }
    public IActionResult PageDetail(long id)
    {
        var page = _pageRepository.Query().FirstOrDefault(x => x.Id == id);

        if (page == null)
        {
            return NotFound();
        }

        var model = new PageVm
        {
            Name = page.Name,
            Body = page.Body,
            MetaTitle = page.MetaTitle,
            MetaKeywords = page.MetaKeywords,
            MetaDescription = page.MetaDescription
        };

        return View(model);
    }
}

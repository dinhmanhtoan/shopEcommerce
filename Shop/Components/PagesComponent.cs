using HtmlAgilityPack;

namespace Shop.Components;

public class PagesViewComponent :ViewComponent
{
    private readonly IRepository<Pages> _pagesRepository;
    public PagesViewComponent(IRepository<Pages> pagesRepository)
    {
        _pagesRepository = pagesRepository;
    }
    public IViewComponentResult Invoke()
    {

        var query = _pagesRepository.Query().ToList();
        var Pages = new List<PageVm>();
        foreach (var item in query)
        {
            var doc = new HtmlDocument();
            string tableTag = item.Body;
            doc.LoadHtml(tableTag);
            var img = doc.DocumentNode.SelectSingleNode("//img");
            var text = doc.DocumentNode.SelectSingleNode("//p");
            string link = img.Attributes["src"].Value;
            var Page = new PageVm()
            {
                Name = item.Name,
                Slug = item.Slug,
                MetaDescription = item.MetaDescription,
                MetaTitle = item.MetaTitle,
                MetaKeywords = item.MetaKeywords,
                href = link,
                Body = item.Body,
                shortContent = text != null ? text.InnerText : ""
            };
            Pages.Add(Page);
        }
   
        return View(Pages);
    }
}


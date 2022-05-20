using System.Linq.Dynamic.Core;
using System.Net.Http.Headers;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="admin")]
[Route("[controller]/[action]")]
public class NewsItemListController : Controller
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IRepository<NewsCategory> _newsCategoryRepository;
    private readonly IMediaService _mediaService;
    private readonly INewsItemService _newsItemService;
    private readonly IWorkContext _workContext;
    public NewsItemListController(IRepository<NewsItem> newsItemRepository, 
        IRepository<NewsCategory> newsCategoryRepository, IMediaService mediaService,
        INewsItemService newsItemService, IWorkContext workContext)
    {
        _newsItemRepository = newsItemRepository;
        _newsCategoryRepository = newsCategoryRepository;
        _mediaService = mediaService;
        _newsItemService = newsItemService;
        _workContext = workContext;
    }
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public ActionResult query()
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
        var NewsItem = _newsItemRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            NewsItem = NewsItem.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            NewsItem = NewsItem.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = NewsItem.Count();
        var data = NewsItem.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);

    }
    [HttpGet]
    public ActionResult Create()
    {
        var Item = _newsCategoryRepository.Query().Where(x=> !x.IsDeleted).ToList();
        var newsItemForm = new NewsItemForm();
        newsItemForm.NewsCategoryIds = Item.Select(x=> x.Id).ToList();
        newsItemForm.NewsCategorys = Item;
        return View(newsItemForm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NewsItemForm model)
    {
        if (ModelState.IsValid)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var newsItem = new NewsItem()
            {
                Name = model.Name,
                Slug = model.Slug,
                MetaTitle = model.MetaTitle,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                ShortContent = model.ShortContent,
                FullContent = model.FullContent,
                CreatedBy = currentUser,
                LatestUpdatedBy = currentUser,
                IsPublished = model.IsPublished
            };
            if (model.IsPublished)
            {
                newsItem.PublishedOn = DateTimeOffset.Now;
            }
            foreach (var newscategoryId in model.NewsCategoryIds)
            {
                var newsItemCategory = new NewsItemCategory
                {
                    CategoryId = newscategoryId
                };
                newsItem.AddNewsItemCategory(newsItemCategory);
            }
            await SaveServiceMedia(model.ThumbnailImage, newsItem);
            _newsItemService.Create(newsItem);
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public ActionResult Edit(long id)
    {
        var newsItem = _newsItemRepository.Query().Include(x=> x.Categories).Include(x=> x.ThumbnailImage).FirstOrDefault(x => x.Id == id);
        if (newsItem == null)
        {
            return NotFound();
        }
   
            var Item = _newsCategoryRepository.Query().Where(x=> !x.IsDeleted).ToList();
            var newsItemForm = new NewsItemForm();
            newsItemForm.Id = newsItem.Id;
            newsItemForm.Name = newsItem.Name;
            newsItemForm.Slug = newsItem.Slug;
            newsItemForm.MetaTitle = newsItem.MetaTitle;
            newsItemForm.MetaKeywords = newsItem.MetaKeywords;
            newsItemForm.MetaDescription = newsItem.MetaDescription;
            newsItemForm.ShortContent = newsItem.ShortContent;
            newsItemForm.FullContent = newsItem.FullContent;
            newsItemForm.IsPublished = newsItem.IsPublished;
            newsItemForm.ThumbnailImageUrl = _mediaService.GetThumbnailUrl(newsItem.ThumbnailImage);
            newsItemForm.NewsCategoryIds = newsItem.Categories.Select(x => x.CategoryId).ToList();
            newsItemForm.NewsCategorys = Item;

            return View(newsItemForm);
      
     
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(long id, NewsItemForm model)
    {
        if (ModelState.IsValid)
        {
            var newsItem = _newsItemRepository.Query().Include(x => x.Categories).Include(x => x.ThumbnailImage).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (newsItem == null)
            {
                return NotFound();
            }
            var currentUser = await _workContext.GetCurrentUser();
            newsItem.Name = model.Name;
            newsItem.Slug = model.Slug;
            newsItem.MetaTitle = model.MetaTitle;
            newsItem.MetaKeywords = model.MetaKeywords;
            newsItem.MetaDescription = model.MetaDescription;
            newsItem.ShortContent = model.ShortContent;
            newsItem.FullContent = model.FullContent;
            newsItem.IsPublished = model.IsPublished;
            newsItem.LatestUpdatedOn = DateTimeOffset.Now;
            newsItem.LatestUpdatedBy = currentUser;
            if (model.IsPublished)
            {
                newsItem.PublishedOn = DateTimeOffset.Now;
            }
            AddOrDeleteNewsItem(model, newsItem);
            if (model.ThumbnailImage != null && newsItem.ThumbnailImage != null)
            {
                await _mediaService.DeleteMediaAsync(newsItem.ThumbnailImage);
            };

            await SaveServiceMedia(model.ThumbnailImage, newsItem);
            _newsItemService.Update(newsItem);
            return Accepted();
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    public ActionResult Delete(long id)
    {
        var newsItem = _newsItemRepository.Query().FirstOrDefault(x => x.Id == id);
        if (newsItem != null)
        {
             _newsItemService.Delete(newsItem);
            return Accepted();
        }
        else
        {
            return NotFound();
        }
    }
    private void AddOrDeleteNewsItem(NewsItemForm newsItemForm, NewsItem newsItem)
    {
        foreach (var CategoryId in newsItemForm.NewsCategoryIds)
        {
            if (newsItem.Categories.Any(x => x.CategoryId == CategoryId))
            {
                continue;
            };
            var newsItemCategory = new NewsItemCategory
            {
                CategoryId = CategoryId
            };
            newsItem.Categories.Add(newsItemCategory);
        }
        var deletedNewsItemCategorys =
                newsItem.Categories.Where(newsItemCategory => !newsItemForm.NewsCategoryIds.Contains(newsItemCategory.CategoryId))
        .ToList();
        foreach (var deletedNewsItemCategory in deletedNewsItemCategorys)
        {
            deletedNewsItemCategory.NewsItem = null;
            newsItem.Categories.Remove(deletedNewsItemCategory);
        }
    }
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
        return fileName;
    }

    private async Task SaveServiceMedia(IFormFile image, NewsItem newsItem)
    {
        if (image != null)
        {
            var fileName = await SaveFile(image);
            if (newsItem.ThumbnailImage != null)
            {
                newsItem.ThumbnailImage.FileName = fileName;
            }
            else
            {
                newsItem.ThumbnailImage = new Media { FileName = fileName };
            }
        }
    }
}


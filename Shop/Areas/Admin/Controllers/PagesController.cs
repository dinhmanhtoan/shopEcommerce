namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class PagesController : Controller
{
    private readonly IRepository<Pages> _pagesRepository;
    private readonly IPageService _pageService;
    private readonly IWorkContext _workContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public PagesController(IRepository<Pages> pagesRepository, IPageService pageService, IWorkContext workContext, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _pagesRepository = pagesRepository;
        _pageService = pageService;
        _workContext = workContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var pages = _pagesRepository.Query().ToList();
        return View(pages);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Pages model)
    {

        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var Pages = new Pages()
            {
                Name = model.Name,
                Slug = model.Slug,
                MetaTitle = model.MetaTitle,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                CreatedBy = model.CreatedBy,
                CreatedById = user.Id,
                LatestUpdatedBy = model.LatestUpdatedBy,
                LatestUpdatedById = user.Id,
                IsPublished = true,
                Body = model.Body
            };
            await _pageService.Create(Pages);
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{Id}")]
    public IActionResult Update(long Id)
    {
        var Pages = _pagesRepository.Query().FirstOrDefault(x => x.Id == Id);
        if(Pages == null)
        {
            return NotFound();
        }
        return View(Pages);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Update(long id,Pages model)
    {
        if (ModelState.IsValid)
        {
            var pages = _pagesRepository.Query().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (pages == null)
            {
                return NotFound();
            }
            var curentUser = await _workContext.GetCurrentUser();
            pages.Name = model.Name;
            pages.Slug = model.Slug;
            pages.MetaTitle = model.MetaTitle;
            pages.MetaDescription = model.MetaDescription;
            pages.MetaKeywords = model.MetaKeywords;
            pages.Body = model.Body;
            pages.LatestUpdatedBy = curentUser;
            pages.LatestUpdatedById = curentUser.Id;
            pages.LatestUpdatedOn = DateTimeOffset.Now;
            pages.PublishedOn = model.PublishedOn;
            pages.IsPublished = model.IsPublished;

            await _pageService.Update(pages);
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long Id)
    {
        var Pages = _pagesRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (Pages == null)
        {
            return NotFound();
        }
        await _pageService.Update(Pages);
        return Accepted();
    }
    [HttpPost]
    public IActionResult Upload()
        {
        var filepath = "";
        foreach (IFormFile photo in Request.Form.Files)
        {
            string serverMapPath = Path.Combine("wwwroot", "images", photo.FileName);
            using (var stream = new FileStream(serverMapPath,FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            filepath = "https//localhost:5001/" + "wwwroot/images/" + photo.FileName;
        }
        return Json(new { url = filepath });
    }
    [HttpPost]
    public ActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
    {
        if (upload.Length <= 0) return null;
        if (!upload.IsImage())
        {
            var NotImageMessage = "please choose a picture";
            dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
            return Json(NotImage);
        }

        var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

        //Image image = Image.FromStream(upload.OpenReadStream());
        //int width = image.Width;
        //int height = image.Height;
        //if ((width > 750) || (height > 500))
        //{
        //    var DimensionErrorMessage = "Custom Message for error";
        //    dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + DimensionErrorMessage + "\"}}");
        //    return Json(stuff);
        //}

        //if (upload.Length > 500 * 1024)
        //{
        //    var LengthErrorMessage = "Custom Message for error";
        //    dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + LengthErrorMessage + "\"}}");
        //    return Json(stuff);
        //}

        var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/images/CKEditorImages",
            fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            upload.CopyTo(stream);

        }

        var url = $"{"/images/CKEditorImages/"}{fileName}";
        return Json(new { url = url });
    }

}


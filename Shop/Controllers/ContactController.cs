namespace Shop.Controllers;

public class ContactController : Controller
{
    private readonly IRepository<Contact> _contactRepository;
    public ContactController(IRepository<Contact> contactRepository)
    {
        _contactRepository = contactRepository;
    }
    // GET: ContactController/Details/5
    public ActionResult Index()
    {
        return View();
    }


    // POST: ContactController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Index(ContactForm model)
    {

        
        if (ModelState.IsValid)
        {
            var Contact = new Contact()
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Note = model.Note,
                IsDeleted = false,
                CreatedOn = DateTimeOffset.Now
            };
            _contactRepository.Add(Contact);
            _contactRepository.SaveChanges();
            return RedirectToAction(nameof(Success));
        }
         
            return View(model);

    }

    // GET: ContactController/Edit/5
    public ActionResult Success()
    {
        return View();
    }


}


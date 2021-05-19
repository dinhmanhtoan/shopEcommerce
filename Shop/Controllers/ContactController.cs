using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModel;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ContactController : Controller
    {
        private readonly shopContext _context;
        public ContactController(shopContext context)
        {
            _context = context;
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
                _context.Contact.Add(Contact);
                _context.SaveChanges();
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
}

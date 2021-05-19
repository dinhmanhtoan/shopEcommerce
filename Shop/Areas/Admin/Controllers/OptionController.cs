using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    [Authorize(Roles = "admin")]
    public class OptionController : Controller
    {
        private readonly ICategoryService _CategoryServices;
        private readonly IProductService _ProductServices;
        private readonly IMediaService _mediaService;
        private readonly IOptionService _optionService;
        private readonly shopContext _context;
        public OptionController(ICategoryService CategoryServices, IProductService ProductServices, IMediaService mediaService, shopContext context, IOptionService optionService)
        {
            _CategoryServices = CategoryServices;
            _ProductServices = ProductServices;
            _mediaService = mediaService;
            _context = context;
            _optionService = optionService;
        }
        // GET: OptionController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var option = await _optionService.GetAll();
            return View(option);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductOption option)
        {
            if (ModelState.IsValid)
            {
                _optionService.AddOption(option);
                return RedirectToAction("Index", "Option");
            }
            return View(option);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Updated(long Id)
        {
            var option = _optionService.getById(Id);
            return View(option);
        }
        [HttpPost("{Id}")]
        public async Task<IActionResult> Updated(long Id, ProductOption option)
        {
            //var query = _optionService.getById(Id); // tracking  không biết làm sao bị
            //if (query != null)
            //{
            if (ModelState.IsValid)
            {
                _optionService.UpdateOption(option); // thêm asnotracking() nên không bị những  query ở trên phải bỏ đi
                return RedirectToAction("Index", "Option");
            }
               
            //}
          
            return NotFound();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Deleted(long Id)
        {
            var option =  _optionService.getById(Id);
            return View(option);
        }
        [HttpPost]
        public async Task<IActionResult> DeletedById(long Id)
        {
            _optionService.Delete(Id);
            return RedirectToAction("Index","Option");
        }

    }
}

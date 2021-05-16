using Microsoft.AspNetCore.Http;
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
    public class BrandController : Controller
    {
        private readonly ICategoryService _CategoryServices;
        private readonly IProductService _ProductServices;
        private readonly IMediaService _mediaService;
        private readonly IBrandService _brandService;
        private readonly shopContext _context;
        public BrandController(ICategoryService CategoryServices, IProductService ProductServices, IMediaService mediaService, shopContext context, IBrandService brandService)
        {
            _CategoryServices = CategoryServices;
            _ProductServices = ProductServices;
            _mediaService = mediaService;
            _context = context;
            _brandService = brandService;
        }
        // GET: BrandController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Brand = await _brandService.GetAll();
            return View(Brand);
        }

        [HttpGet]
        // GET: BrandController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand model)
        {
            if (ModelState.IsValid)
            {
                _brandService.AddBrand(model);
                return RedirectToAction("Index", "Brand");
            }
            return View(model);

        }
        // GET: BrandController/Details/5
        [HttpGet("{Id}")]
        public IActionResult Details(long Id)
        {
            var Brand = _brandService.getById(Id);
            return View(Brand);
        }
        // GET: BrandController/Edit/5
        [HttpGet("{Id}")]
        public IActionResult Updated(long id)
        {
            var Brand = _brandService.getById(id);
            if (Brand != null)
            {
                return View(Brand);
            }
            return NotFound("Brand Không Tồn Tại Vui Lòng Tải Lại Trang");

        }

        // POST: BrandController/Edit/5
        [HttpPost("{Id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Updated(long Id, Brand model)
        {
            if (ModelState.IsValid)
            {
                _brandService.UpdateBrand(model);
                return RedirectToAction("Index", "Brand");
            }
            return View(model);
        }

        // GET: BrandController/Delete/5
        [HttpGet("{Id}")]
        public IActionResult Deleted(long id)
        {
            var Brand = _brandService.getById(id);
            return View(Brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletedById(long id)
        {
            _brandService.Delete(id);
             return RedirectToAction("Index", "Brand");
        }
    }
}

using Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Admin.Helpers;

namespace Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryServices;
        private readonly IProductService _ProductServices;
        private readonly IMediaService _mediaService;
        public CategoryController(ICategoryService CategoryServices, IProductService ProductServices, IMediaService mediaService)
        {
            _CategoryServices = CategoryServices;
            _ProductServices = ProductServices;
            _mediaService = mediaService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var Category = await _CategoryServices.GetAll();
            return View(Category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var formCategory = new FormCategory();
            formCategory.Categorys.Code = stringHelper.Generate(8);
            return View(formCategory);
        }
        [HttpPost]
        public  IActionResult Create(FormCategory model)
        {
          
            if (ModelState.IsValid)
            {
                var Category = new Category()
                {
                    Code = model.Categorys.Code,
                    Title = model.Categorys.Title,
                    Description = model.Categorys.Description,
                    CreatedOn = DateTime.Now,
                    //  CreatedBy = model..CategorysTitle,
                };
                SaveCategory(model, Category);
                _CategoryServices.AddCategory(Category);
                return Redirect("/Admin/Category/Index");
            }
            return View();  
        }
        [HttpGet]
        public async Task<IActionResult> Details(long Id)
        {
            var Category = await _CategoryServices.getById(Id);
            if (Category != null)
            {
                return View(Category);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Updated(long Id )
        {
            var Category = await  _CategoryServices.getById(Id);
            var CategoryVm = new CategoryVm()
            {
                Id = Category.Id,
                Code = Category.Code,
                Title = Category.Title,
                Description = Category.Description,
                CreatedOn = DateTime.Now,
                //  CreatedBy = model..CategorysTitle,
            };
            var FormCategory = new FormCategory();
                FormCategory.Categorys = CategoryVm;
            return View(FormCategory);
        }
        [HttpPost("{Id}")]
        public IActionResult Updated(FormCategory model)
        {
            var Category = new Category()
            {
                Code = model.Categorys.Code,
                Title = model.Categorys.Title,
                Description = model.Categorys.Description,
                EditOn = DateTime.Now,
                //  UpdateBy = model.UpdateBy,
            };
            SaveCategory(model, Category);
            _CategoryServices.UpdateCategory(Category);
            return Redirect("/Admin/Category/Index");
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Deleted(long Id)
        {
            var category = await _CategoryServices.getById(Id);

            return View(category);
        }
        [HttpPost]
        public IActionResult DeletedById(long Id)
        {
            _CategoryServices.Delete(Id);
            return Redirect("/Admin/Category/Index");
        }
        public async Task SaveCategory(FormCategory model, Category category)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (category.Thumbnail != null)
                {
                    category.Thumbnail.FileName = fileName;
                }
                else
                {
                    category.Thumbnail = new Media { FileName = fileName };
                }
        
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}

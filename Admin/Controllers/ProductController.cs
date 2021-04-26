using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Admin.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]

    public class ProductController : Controller
    {
        private readonly IProductService _ProductServices;
        private readonly ICategoryService _CategoryServices;
        private readonly IMediaService _mediaService;
        private readonly shopContext _context;
        public ProductController(IProductService ProductServices, ICategoryService CategoryServices, IMediaService mediaService, shopContext context)
        {
            _ProductServices = ProductServices;
            _CategoryServices = CategoryServices;
            _mediaService = mediaService;
            _context = context;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var Product = await _ProductServices.GetAll();
            return View(Product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Category = await _CategoryServices.GetAll();
            var FormProduct = new FormProduct();
            var Product = new ProductVm();
            Product.Code = stringHelper.Generate(8);
            Product.categories = Category;
            FormProduct.Products = Product;
            return View(FormProduct);
        }
        [HttpPost]

        public async Task<IActionResult> Create(FormProduct model)
        {
            if (ModelState.IsValid)
            {
                var Product = new Product()
                {
                    Code = model.Products.Code,
                    Title = model.Products.Title,
                    Slug = model.Products.Slug,
                    Description = model.Products.Description,
                    Detail = model.Products.Detail,
                    Price = model.Products.Price,
                    CategoryId = model.Products.CategoryId,
                    Sale = model.Products.Sale,
                    CreatedOn = DateTime.Now,
                    //  CreatedBy = model.Title,
                };
                await SaveImage(model, Product);
                await _ProductServices.AddProduct(Product);
                return Redirect("/Admin/Product/Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long Id)
        {

            var Product = await _ProductServices.getById(Id);

            if (Product != null)
            {
                return View(Product);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Updated(long Id)
        {
            var Product = await _ProductServices.getById(Id);
            var ListImage = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
            var Category = await _CategoryServices.GetAll();
            var FormProduct = new FormProduct();
            var ProductVm = new ProductVm()
            {
                Id = Product.Id,
                Code = Product.Code,
                Title = Product.Title,
                Slug = Product.Slug,
                Description = Product.Description,
                Detail = Product.Detail,
                categories = Category,
                CategoryId = Product.CategoryId,
                Price = Product.Price,
                Sale = Product.Sale,
                ThumbnailImageUrl = Product.Thumbnail.FileName
            };

            foreach (var productMedia in ListImage.Images.Where(x => x.Media.MediaType == MediaType.Image))
            {
                ProductVm.ProductImages.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                    Media = productMedia.Media.FileName
                });
            }
            FormProduct.Products = ProductVm;
            return View(FormProduct);
        }
        [HttpPost("{Id}")]
        public async Task<IActionResult> Updated(long Id, FormProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _ProductServices.getById(Id).Result;
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                product.Code = model.Products.Code;
                product.Title = model.Products.Title;
                product.Slug = model.Products.Slug;
                product.Description = model.Products.Description;
                product.Detail = model.Products.Detail;
                product.CategoryId = model.Products.CategoryId;
                product.Price = model.Products.Price;
                product.Sale = model.Products.Sale;
                product.EditOn = DateTime.Now;
           
                await SaveImage(model, product);
                await _ProductServices.UpdateProduct(product);
                return Redirect("/Admin/Product/Index");
            }
            return View(ModelState);


        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Deleted(long Id)
        {
            var Product = await _ProductServices.getById(Id);
            return View(Product);
        }
        [HttpPost]
        public IActionResult DeletedById(long Id)
        {
            var ProductImages = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media)
                                    .Include(x => x.Thumbnail).FirstOrDefault();
            if (ProductImages == null)
            {
                ModelState.AddModelError("error", "Sản phẩm không tồn tại");
                  return BadRequest();
            }
            foreach (var item in ProductImages.Images)
            {
                _context.ProductMedias.Remove(item);
                 _context.Medias.Remove(item.Media);
                RemoveFile(item.Media.FileName);
                
            }
            if (ProductImages.Thumbnail != null)
            {
                _context.Medias.Remove(ProductImages.Thumbnail);
            }
            _ProductServices.Delete(Id);
            return Redirect("/Admin/Product/Index");
        }
        public async Task SaveImage(FormProduct model, Product product)
        {
            var ProductImages = _context.Product.Where(x => x.Id == product.Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
           
          
            if (model.ThumbnailImage != null)
            {
                var ThumbnailImage = _context.Product.Include(x => x.Thumbnail).FirstOrDefault();
                if (!(ThumbnailImage.Thumbnail.FileName == model.ThumbnailImage.FileName))
                {

                    var fileName = await SaveFile(model.ThumbnailImage);
                    if (product.Thumbnail != null)
                    {
                        RemoveFile(product.Thumbnail.FileName);
                        product.Thumbnail.FileName = fileName;
                     
                    }
                    else
                    {
                  
                        product.Thumbnail = new Media { FileName = fileName };
                    }
                }
            }
          

            if (model.ListDelete != null)
            {
                var ListDelete = JsonConvert.DeserializeObject<List<string>>(model.ListDelete);
                foreach(var delete in ListDelete)
                {
                    foreach (var media in ProductImages.Images)
                    {
                        if (media.Media != null)
                        {
                            if (media.Media.FileName == delete)
                            {
                                RemoveFile(delete);
                                var deleteMedia = _context.Medias.Where(x => x.FileName == delete).FirstOrDefault();
                                var ProductMedias = _context.ProductMedias.Where(x => x.MediaId == deleteMedia.Id).FirstOrDefault();
                                _context.ProductMedias.Remove(ProductMedias);

                            }
                         }
                    }
                
                }
            }
            foreach (var file in model.ProductImages)
            {
   
                if (file.FileName != null)
                {
                    var fileName = await SaveFile(file);
                    var productMedia = new ProductMedia
                    {
                        Product = product,
                        Media = new Media { FileName = fileName, MediaType = MediaType.Image }
                    };
                    product.AddMedia(productMedia);
                }
            }
        }

        private void  RemoveFile(string file)
        {
            _mediaService.DeleteMediaAsync(file);
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

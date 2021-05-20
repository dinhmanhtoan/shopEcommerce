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
using Shop.Areas.Admin.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _ProductServices;
        private readonly ICategoryService _CategoryServices;
        private readonly IBrandService _brandServices;
        private readonly IMediaService _mediaService;
        private readonly IOptionService _optionService;
        private readonly shopContext _context;
        public ProductController(IProductService ProductServices, ICategoryService CategoryServices, IMediaService mediaService, shopContext context,
            IBrandService brandServices, IOptionService optionService)
        {
            _ProductServices = ProductServices;
            _CategoryServices = CategoryServices;
            _mediaService = mediaService;
            _context = context;
            _brandServices = brandServices;
            _optionService = optionService;
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
            var Brand = await _brandServices.GetAll();
            var option = await _optionService.GetAll();
            var FormProduct = new FormProduct();
            var Product = new ProductVm();
            Product.Code = stringHelper.Generate(8);
            Product.categories = Category;
            Product.brands = Brand;
            FormProduct.Products = Product;
            var ProductOptionVms = new List<ProductOptionVm>();
            foreach (var item in option)
            {
                var ProductOptionVm = new ProductOptionVm()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                ProductOptionVms.Add(ProductOptionVm);
            }
            FormProduct.ProductOptionVm = ProductOptionVms;
            return View(FormProduct);
        }
        [HttpPost]

        public async Task<IActionResult> Create(FormProduct model)
        {
            if (model.Products.Sale != null)
            {
                if (model.Products.Price < model.Products.Sale)
                {
                    ModelState.AddModelError("Products.Price", "Giá Tiền Không Được Nhỏ Hơn Giá Khuyễn Mãi");
                }
            }
            if (model.Products.CategoryId == -1)
            {
                model.Products.CategoryId = null;
            }
            if (model.Products.BrandId ==-1)
            {
                model.Products.BrandId = null;
            }
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
                    BrandId = model.Products.BrandId,
                    CategoryId = model.Products.CategoryId,
                    Sale = model.Products.Sale,
                    IsFuture = model.Products.IsFuture,
                    IsHot = model.Products.IsHot,
                    CreatedOn = DateTime.Now
                };
                var optionIndex = 0;
                foreach (var option in model.ProductOptionVm)
                {
                    
                    foreach (var item in option.Values)
                    {
                        if (item.Key != null)
                        {
                            Product.AddOptionValue(new ProductOptionValue
                            {
                                OptionId = option.Id,
                                DisplayType = option.DisplayType,
                                Value = JsonConvert.SerializeObject(option.Values),
                                SortIndex = optionIndex
                            });
                        }
                    }
                    optionIndex++;
                }
                await SaveImage(model, Product);
                await _ProductServices.AddProduct(Product);
                return RedirectToAction("Index");
            }
            var Category = await _CategoryServices.GetAll();
            var Brand = await _brandServices.GetAll();
            var option2 = await _optionService.GetAll();
            model.Products.categories = Category;
            model.Products.brands = Brand;
            var ProductOptionVms = new List<ProductOptionVm>();
            foreach (var item in option2)
            {
                var ProductOptionVm = new ProductOptionVm()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                ProductOptionVms.Add(ProductOptionVm);
            }
            model.ProductOptionVm = ProductOptionVms;
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
            var product = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media)
                .Include(x=> x.Thumbnail)
                                      .Include(x => x.Category)
                                  .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Brand).FirstOrDefault(x => x.Id == Id);
            var option = await _optionService.GetAll();
            var ListImage = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
            var Category = await _CategoryServices.GetAll();
            var Brand = await _brandServices.GetAll();
            var FormProduct = new FormProduct();
            var ProductVm = new ProductVm()
            {
                Id = product.Id,
                Code = product.Code,
                Title = product.Title,
                Slug = product.Slug,
                Description = product.Description,
                Detail = product.Detail,
                categories = Category,
                BrandId = product.BrandId,
                brands = Brand,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Sale = product.Sale,
                IsFuture = product.IsFuture,
                IsHot = product.IsHot,
                ThumbnailImageUrl = product.Thumbnail != null ? product.Thumbnail.FileName : ""
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
            var ProductOptionVms = new List<ProductOption>();
            foreach (var item in option)
            {
                var ProductOption = new ProductOption()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                
                ProductOptionVms.Add(ProductOption);
            }
            FormProduct.ProductOption = ProductOptionVms;
            FormProduct.ProductOptionVm = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
           new ProductOptionVm
           {
               Id = x.OptionId,
               Name = x.Option.Name,
               DisplayType = x.DisplayType,
               Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
           }).ToList();

            return View(FormProduct);
        }
        [HttpPost("{Id}")]
        public async Task<IActionResult> Updated(long Id, FormProduct model)
        {
            var product = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media)
                                   .Include(x => x.Category).Include(x => x.Thumbnail)
                               .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Brand).FirstOrDefault(x => x.Id == Id);
            var option = _context.ProductOption.ToList();
            var ListImage = _context.Product.Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
            if (model.Products.Price < model.Products.Sale)
            {
                ModelState.AddModelError("Products.Price", "Giá Tiền Không Được Nhỏ Hơn Giá Khuyễn Mãi");
            }    
   
            //var product = _ProductServices.getById(Id).Result;
            if (product == null)
            {
                return NotFound();
            }
       
            if (model.Products.CategoryId == -1)
            {
                model.Products.CategoryId = null;
            }
            if (model.Products.BrandId == -1)
            {
                model.Products.BrandId = null;
            }
            if (ModelState.IsValid)
            {
                product.Id = model.Products.Id;
                product.Code = model.Products.Code;
                product.Title = model.Products.Title;
                product.Slug = model.Products.Slug; // bị tracking nhưng thêm result thì không bị nữa
                product.Description = model.Products.Description;
                product.Detail = model.Products.Detail;
                product.CategoryId = model.Products.CategoryId;
                product.BrandId = model.Products.BrandId;
                product.Price = model.Products.Price;
                product.Sale = model.Products.Sale;
                product.IsFuture = model.Products.IsFuture;
                product.IsHot = model.Products.IsHot;
                product.EditOn = DateTime.Now;
           
                await SaveImage(model, product);
                AddOrDeleteProductOption(model, product);
                await _ProductServices.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            if (model.Products.ThumbnailImageUrl == null)
            {
                model.Products.ThumbnailImageUrl = product.Thumbnail.FileName;
            }
            if (model.Products.ProductImages.Count == 0)
            {
                foreach (var productMedia in ListImage.Images.Where(x => x.Media.MediaType == MediaType.Image))
                {
                    model.Products.ProductImages.Add(new ProductMediaVm
                    {
                        Id = productMedia.Id,
                        MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                        Media = productMedia.Media.FileName
                    });
                }
            }
            var ProductOptionVms = new List<ProductOption>();
            foreach (var item in option)
            {
                var ProductOption = new ProductOption()
                {
                    Id = item.Id,
                    Name = item.Name
                };

                ProductOptionVms.Add(ProductOption);
            }
            model.ProductOption = ProductOptionVms;
            model.ProductOptionVm = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
           new ProductOptionVm
           {
               Id = x.OptionId,
               Name = x.Option.Name,
               DisplayType = x.DisplayType,
               Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
           }).ToList();
            return View(model);


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
            return RedirectToAction("Index");
        }
        public async Task SaveImage(FormProduct model, Product product)
        {
            var ProductImages = _context.Product.Where(x => x.Id == product.Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
           
          
            if (model.ThumbnailImage != null)
            {
                var ThumbnailImage = _context.Product.Include(x => x.Thumbnail).FirstOrDefault();
                if (ThumbnailImage == null)
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
                else
                {
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
        private void AddOrDeleteProductOption(FormProduct model, Product product)
        {
            var optionIndex = 0;
            foreach (var optionVm in model.ProductOptionVm)
            {
                var optionValue = product.OptionValues.FirstOrDefault(x => x.OptionId == optionVm.Id);
                if (optionValue == null)
                {
                    foreach (var item in optionVm.Values)
                    {
                        if (item.Key != null)
                        {
                            product.AddOptionValue(new ProductOptionValue
                            {
                                OptionId = optionVm.Id,
                                DisplayType = optionVm.DisplayType,
                                Value = JsonConvert.SerializeObject(optionVm.Values),
                                SortIndex = optionIndex
                            });
                        }
                    }

                }
                else
                {
                    optionValue.Value = JsonConvert.SerializeObject(optionVm.Values);
                    optionValue.DisplayType = optionVm.DisplayType;
                    optionValue.SortIndex = optionIndex;    
                }
                optionIndex++;
            }

            var deletedProductOptionValues = product.OptionValues.Where(x => model.ProductOptionVm.All(vm => vm.Id != x.OptionId)).ToList();

            foreach (var productOptionValue in deletedProductOptionValues)
            {
                product.OptionValues.Remove(productOptionValue);
            }
        }

    }
}

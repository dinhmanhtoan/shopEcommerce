using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;
using Shop.Areas.Admin.Helpers;
using System.Net.Http.Headers;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class ProductController : Controller
{
    private readonly IProductService _ProductServices;
    private readonly ICategoryService _CategoryServices;
    private readonly IBrandService _brandServices;
    private readonly IMediaService _mediaService;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductOption> _productOptionRepository;
    private readonly IRepository<ProductOptionValue> _productOptionValueRepository;
    private readonly IRepository<TaxClass> _taxClassRepository;
    private readonly IRepository<ProductMedia> _productMediaRepository;
    private readonly IRepository<ProductLink> _productLinkRepository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;
    private readonly IRepository<Media> _mediaRepository;
    private readonly IRepository<ProductAttributeGroup> _attributeGroupRepository;
    private readonly IRepository<ProductAttributeValue> _productAttributeValueRepository;
    private readonly IRepository<ProductTemplate> _templateRepository;
    private readonly IWorkContext _workContext;
    public ProductController(IProductService ProductServices, ICategoryService CategoryServices, IMediaService mediaService,
        IRepository<Product> productRepository,
        IRepository<ProductOption> productOptionRepository,
        IRepository<ProductOptionValue> productOptionValueRepository,
        IRepository<TaxClass> taxClassRepository,
        IRepository<ProductMedia> productMediaRepository,
        IRepository<ProductLink> productLinkRepository,
        IRepository<ProductCategory> productCategoryRepository,
        IRepository<Media> mediaRepository,
        IRepository<ProductAttributeGroup> attributeGroupRepository,
        IRepository<ProductTemplate> templateRepository,
        IRepository<ProductAttributeValue> productAttributeValueRepository,
        IBrandService brandServices, IWorkContext workContext)
    {
        _ProductServices = ProductServices;
        _CategoryServices = CategoryServices;
        _mediaService = mediaService;
        _productRepository = productRepository;
        _productOptionRepository = productOptionRepository;
        _productOptionValueRepository = productOptionValueRepository;
        _taxClassRepository = taxClassRepository;
        _brandServices = brandServices;
        _productMediaRepository = productMediaRepository;
        _productLinkRepository = productLinkRepository;
        _productCategoryRepository = productCategoryRepository;
        _mediaRepository = mediaRepository;
        _attributeGroupRepository = attributeGroupRepository;
        _productAttributeValueRepository = productAttributeValueRepository;
        _templateRepository = templateRepository;
        _workContext = workContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> query()
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
        var Product = _productRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Product = Product.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Product = Product.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = Product.Count();
        var data = await Product.Skip(skip).Take(pageSize).ToListAsync();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categoryListList = await _CategoryServices.GetAll();
        var Brand = await _brandServices.GetAll();
        var option = await _productOptionRepository.Query().ToListAsync();
        var TaxClass = _taxClassRepository.Query().ToList();
        var attributeGroup = _attributeGroupRepository.Query().Include(x => x.Attributes).ToList();
        var template = _templateRepository.Query().ToList();

        var TaxClassListItem = TaxClass.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
        var templateListItem = template.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
        var FormProduct = new FormProduct();
        var Product = new ProductVm();
        Product.Sku = stringHelper.Generate(8);
        Product.CategoryListItem = categoryListList;
        Product.TemplateListItem = templateListItem;
        Product.brands = Brand;
        Product.TaxClassListItem = TaxClassListItem;
        foreach (var group in attributeGroup)
        {
            var optionGroup = new SelectListGroup() { Name = group.Name };
            foreach (var attr in group.Attributes)
            {
                Product.AttributesListItem.Add(new SelectListItem() { Value = attr.Id.ToString(), Text = attr.Name, Group = optionGroup });
            }
        }
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
        Product.ProductOption = ProductOptionVms;
        FormProduct.Products = Product;
        return View(FormProduct);
    }
    [HttpPost]
    public async Task<IActionResult> Create(FormProduct model)
    {
        MapUploadedFile(model);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var currentUser = await _workContext.GetCurrentUser();
        var Product = new Product()
        {

            Name = model.Products.Name,
            Slug = model.Products.Slug,
            MetaTitle = model.Products.MetaTitle,
            MetaKeywords = model.Products.MetaKeywords,
            MetaDescription = model.Products.MetaDescription,
            Sku = model.Products.Sku,
            Gtin = model.Products.Gtin,
            Description = model.Products.Description,
            ShortDescription = model.Products.ShortDescription,
            Specification = model.Products.Specification,
            Price = model.Products.Price,
            OldPrice = model.Products.OldPrice,
            SpecialPrice = model.Products.SpecialPrice,
            Present = model.Products.Present,
            SpecialPriceStart = model.Products.SpecialPriceStart,
            SpecialPriceEnd = model.Products.SpecialPriceEnd,
            TaxClassId = model.Products.TaxClassId,
            BrandId = model.Products.BrandId,
            IsFuture = model.Products.IsFuture,
            IsHot = model.Products.IsHot,
            HasOptions = model.Products.Variations.Any() ? true : false,
            IsAllowToOrder = model.Products.IsAllowToOrder,
            StockTrackingIsEnabled = model.Products.StockTrackingIsEnabled,
            IsVisibleIndividually = true,
            IsPublished = model.Products.IsPublished,
            IsDeleted = false,
            CreatedOn = DateTime.Now,
            CreatedBy = currentUser.Id,
        };
        if (!User.IsInRole("admin"))
        {
            Product.VendorId = currentUser.VendorId;
        }

        var optionIndex = 0;
        foreach (var option in model.Products.Options)
        {
            Product.AddOptionValue(new ProductOptionValue
            {
                OptionId = option.Id,
                DisplayType = option.DisplayType,
                Value = JsonConvert.SerializeObject(option.Values),
                SortIndex = optionIndex
            });
            optionIndex++;
        }

        foreach (var attribute in model.Products.Attributes)
        {
            var attributeValue = new ProductAttributeValue
            {
                AttributeId = attribute.Id,
                Value = attribute.Value
            };

            Product.AddAttributeValue(attributeValue);
        }
        foreach (var categoryId in model.Products.CategoryIds)
        {
            var productCategory = new ProductCategory
            {

                CategoryId = categoryId
            };
            Product.AddCategory(productCategory);

        }
        await SaveProductMedias(model, Product);
        await MapProductVariationVmToProduct(currentUser, model, Product);
        MapProductLinkVmToProduct(model, Product);

        var productPriceHistory = CreatePriceHistory(currentUser, Product);
        Product.PriceHistories.Add(productPriceHistory);
        _ProductServices.AddProduct(Product);
        return CreatedAtAction(nameof(Index), null, null);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Updated(long Id)
    {
        var product = _productRepository.Query().Where(x => x.Id == Id)
            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.Thumbnail)
            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(i => i.Images).ThenInclude(m => m.Media)
            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(o => o.OptionCombinations)
            .Include(x => x.Images).ThenInclude(x => x.Media)
            .Include(x => x.AttributeValues).ThenInclude(a => a.Attribute).ThenInclude(g => g.Group)
            .Include(x => x.Thumbnail)
            .Include(x => x.Categories)
            .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Brand).Where(x => !x.IsDeleted).FirstOrDefault();
        if (product == null)
        {
            return NotFound();
        }
        var option = await _productOptionRepository.Query().ToListAsync();
        var ListImage = _productRepository.Query().Where(x => x.Id == Id).Include(x => x.Images).ThenInclude(x => x.Media).FirstOrDefault();
        var categoryListList = await _CategoryServices.GetAll();
        var Brand = await _brandServices.GetAll();
        var ProductVm = new ProductVm()
        {
            Id = product.Id,
            Sku = product.Sku,
            Gtin = product.Gtin,
            Name = product.Name,
            MetaTitle = product.MetaTitle,
            MetaDescription = product.MetaDescription,
            MetaKeywords = product.MetaKeywords,
            Slug = product.Slug,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Specification = product.Specification,
            Price = product.Price,
            OldPrice = product.OldPrice,
            SpecialPrice = product.SpecialPrice,
            Present = product.Present,
            SpecialPriceStart = product.SpecialPriceStart,
            SpecialPriceEnd = product.SpecialPriceEnd,
            BrandId = product.BrandId,
            CategoryIds = product.Categories.Select(x => x.CategoryId).ToList(),
            TaxClassId = product.TaxClassId,
            IsDeleted = product.IsDeleted,
            IsFuture = product.IsFuture,
            IsHot = product.IsHot,
            IsAllowToOrder = product.IsAllowToOrder,
            IsPublished = product.IsPublished,
            StockTrackingIsEnabled = product.StockTrackingIsEnabled,
            ThumbnailImageUrl = _mediaService.GetThumbnailUrl(product.Thumbnail),
        };
        ProductVm.brands = Brand;
        ProductVm.CategoryListItem = categoryListList;
        var TaxClass = _taxClassRepository.Query().ToList();
        var TaxClassListItem = TaxClass.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString(),
            Selected = x.Id == ProductVm.TaxClassId ? true : false

        }).ToList();
        ProductVm.TaxClassListItem = TaxClassListItem;
        foreach (var productMedia in product.Images.Where(x => x.Media.MediaType == MediaType.Image))
        {
            ProductVm.ProductImages.Add(new ProductMediaVm
            {
                Id = productMedia.Id,
                MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media)
            });
        }
        var ProductOptions = new List<ProductOptionVm>();
        foreach (var item in option)
        {
            var ProductOption = new ProductOptionVm()
            {
                Id = item.Id,
                Name = item.Name
            };

            ProductOptions.Add(ProductOption);
        }
        ProductVm.Options = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
       new ProductOptionVm
       {
           Id = x.OptionId,
           Name = x.Option.Name,
           DisplayType = x.DisplayType,
           Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
       }).ToList();

        ProductVm.ProductOption = ProductOptions;
        var template = _templateRepository.Query().ToList();
        var templateListItem = template.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
        ProductVm.TemplateListItem = templateListItem;
        var attributeGroup = _attributeGroupRepository.Query().Include(x => x.Attributes).ToList();
        foreach (var group in attributeGroup)
        {
            var optionGroup = new SelectListGroup() { Name = group.Name };
            foreach (var attr in group.Attributes)
            {
                ProductVm.AttributesListItem.Add(new SelectListItem() { Value = attr.Id.ToString(), Text = attr.Name, Group = optionGroup });
            }
        }
        foreach (var variation in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Super).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
        {
            ProductVm.Variations.Add(new ProductVariationVm
            {
                Id = variation.Id,
                Name = variation.Name,
                NormalizedName = variation.NormalizedName,
                Sku = variation.Sku,
                Gtin = variation.Gtin,
                Price = variation.Price,
                OldPrice = variation.OldPrice,
                ThumbnailImageUrl = _mediaService.GetMediaUrl(variation.Thumbnail),
                ImageUrls = GetProductImageUrls(variation.Id).ToList(),
                ImageVariation = variation.Images.Where(x => x.Media.MediaType == MediaType.Image).Select(x => new ProductMediaVm
                {
                    Id = x.Id,
                    MediaUrl = _mediaService.GetThumbnailUrl(x.Media)
                }).ToList(),
                OptionCombinations = variation.OptionCombinations.Select(x => new ProductOptionCombinationVm
                {
                    OptionId = x.OptionId,
                    OptionName = x.Option.Name,
                    Value = x.Value,
                    SortIndex = x.SortIndex
                }).OrderBy(x => x.SortIndex).ToList()
            });
        }
        foreach (var relatedProduct in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
        {
            ProductVm.RelatedProducts.Add(new ProductLinkVm
            {
                Id = relatedProduct.Id,
                Name = relatedProduct.Name,
                IsPublished = relatedProduct.IsPublished
            });
        }

        foreach (var crossSellProduct in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
        {
            ProductVm.CrossSellProducts.Add(new ProductLinkVm
            {
                Id = crossSellProduct.Id,
                Name = crossSellProduct.Name,
                IsPublished = crossSellProduct.IsPublished
            });
        }

        ProductVm.Attributes = product.AttributeValues.Select(x => new ProductAttributeVm
        {
            AttributeValueId = x.Id,
            Id = x.AttributeId,
            Name = x.Attribute.Name,
            GroupName = x.Attribute.Group.Name,
            Value = x.Value
        }).ToList();
        var FormProduct = new FormProduct();
        FormProduct.Products = ProductVm;
        return View(FormProduct);
    }
    [HttpPost("{Id}")]
    public async Task<IActionResult> Updated(long Id, FormProduct model)
    {
        MapUploadedFile(model);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var product = _productRepository.Query()
                            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.Thumbnail)
                            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.Images).ThenInclude(x => x.Media)
                            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(o => o.OptionCombinations)
                            .Include(x => x.Images).ThenInclude(x => x.Media)
                            .Include(x => x.Categories).ThenInclude(x => x.Category)
                            .Include(x => x.AttributeValues).ThenInclude(a => a.Attribute).ThenInclude(g => g.Group)
                            .Include(x => x.Thumbnail)
                            .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Brand).FirstOrDefault(x => x.Id == Id);
        if (product == null)
        {
            return NotFound();
        }
        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this product" });
        }
        var isPriceChanged = false;
        if (product.Price != model.Products.Price ||
            product.OldPrice != model.Products.OldPrice ||
            product.SpecialPrice != model.Products.SpecialPrice ||
            product.SpecialPriceStart != model.Products.SpecialPriceStart ||
            product.SpecialPriceEnd != model.Products.SpecialPriceEnd)
        {
            isPriceChanged = true;
        }

        product.Sku = model.Products.Sku;
        product.Gtin = model.Products.Gtin;
        product.Name = model.Products.Name;
        product.MetaTitle = model.Products.MetaTitle;
        product.MetaKeywords = model.Products.MetaKeywords;
        product.MetaDescription = model.Products.MetaDescription;
        product.Slug = model.Products.Slug;
        product.Description = model.Products.Description;
        product.ShortDescription = model.Products.ShortDescription;
        product.Specification = model.Products.Specification;
        product.Price = model.Products.Price;
        product.OldPrice = model.Products.OldPrice;
        product.SpecialPrice = model.Products.SpecialPrice;
        product.Present = model.Products.Present;
        product.SpecialPriceStart = model.Products.SpecialPriceStart;
        product.SpecialPriceEnd = model.Products.SpecialPriceEnd;
        product.BrandId = model.Products.BrandId;
        product.IsFuture = model.Products.IsFuture;
        product.IsHot = model.Products.IsHot;
        product.TaxClassId = model.Products.TaxClassId;
        product.EditOn = DateTime.Now;
        product.EditBy = currentUser.Id;
        product.IsPublished = model.Products.IsPublished;
        product.IsAllowToOrder = model.Products.IsAllowToOrder;
        product.StockTrackingIsEnabled = model.Products.StockTrackingIsEnabled;
        product.HasOptions = model.Products.Variations.Any() ? true : false;

        if (isPriceChanged)
        {
            var productPriceHistory = CreatePriceHistory(currentUser, product);
            product.PriceHistories.Add(productPriceHistory);
        }

        await SaveProductMedias(model, product);

        foreach (var productMediaId in model.Products.DeletedMediaIds)
        {
            var productMedia = product.Images.First(x => x.Id == productMediaId);
            _productMediaRepository.Remove(productMedia);
            await _mediaService.DeleteMediaAsync(productMedia.Media);
        }

        AddOrDeleteProductOption(model, product);
        AddOrDeleteCategories(model, product);
        AddOrDeleteProductAttribute(model, product);

        await AddOrDeleteProductVariation(currentUser, model, product);
        AddOrDeleteProductLinks(model, product);

        await _ProductServices.UpdateProduct(product);

        return CreatedAtAction(nameof(Index), null, null);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long Id)
    {
        var Product = await _productRepository.Query().FirstOrDefaultAsync();
        if (Product == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && Product.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this product" });
        }

        await _ProductServices.Delete(Id);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(long id)
    {
        var product = _productRepository.Query().FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this product" });
        }

        product.IsPublished = !product.IsPublished;
        await _productRepository.SaveChangesAsync();

        return Accepted();
    }
    private IEnumerable<string> GetProductImageUrls(long productId)
    {
        var imageUrls = _productMediaRepository.Query()
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Media)
            .ToList()
            .Select(x => _mediaService.GetMediaUrl(x));

        return imageUrls;
    }
    private void RemoveFile(string file)
    {
        _mediaService.DeleteMediaAsync(file);
    }
    private async Task MapProductVariationVmToProduct(User loginUser, FormProduct model, Product product)
    {
        foreach (var variationVm in model.Products.Variations)
        {
            var productLink = new ProductLink
            {
                LinkType = ProductLinkType.Super,
                Product = product,
                LinkedProduct = product.Clone()
            };

            productLink.LinkedProduct.CreatedBy = loginUser.Id;
            productLink.LinkedProduct.EditBy = loginUser.Id;
            productLink.LinkedProduct.Name = variationVm.Name;
            productLink.LinkedProduct.NormalizedName = variationVm.NormalizedName;
            productLink.LinkedProduct.Slug = variationVm.Name.ToUrlFriendly();
            productLink.LinkedProduct.Sku = variationVm.Sku;
            productLink.LinkedProduct.Gtin = variationVm.Gtin;
            productLink.LinkedProduct.Price = variationVm.Price;
            productLink.LinkedProduct.OldPrice = variationVm.OldPrice;
            productLink.LinkedProduct.IsDeleted = false;
            productLink.LinkedProduct.HasOptions = false;
            productLink.LinkedProduct.IsVisibleIndividually = false;

            if (product.Thumbnail != null)
            {
                productLink.LinkedProduct.Thumbnail = new Media { FileName = product.Thumbnail.FileName };
            }

            await MapProductVariantImageFromVm(variationVm, productLink.LinkedProduct);
            foreach (var combinationVm in variationVm.OptionCombinations)
            {
                productLink.LinkedProduct.AddOptionCombination(new ProductOptionCombination
                {
                    OptionId = combinationVm.OptionId,
                    Value = combinationVm.Value,
                    SortIndex = combinationVm.SortIndex
                });
            }
            var productPriceHistory = CreatePriceHistory(loginUser, productLink.LinkedProduct);
            product.PriceHistories.Add(productPriceHistory);
            product.AddProductLinks(productLink);
        }
    }
    private async Task MapProductVariantImageFromVm(ProductVariationVm variationVm, Product product)
    {
        if (variationVm.ThumbnailImage != null)
        {
            var thumbnailImageFileName = await SaveFile(variationVm.ThumbnailImage);
            if (product.Thumbnail != null)
            {
                product.Thumbnail.FileName = thumbnailImageFileName;
            }
            else
            {
                product.Thumbnail = new Media { FileName = thumbnailImageFileName };
            }
        }

        foreach (var image in variationVm.NewImages)
        {
            var fileName = await SaveFile(image);
            var productMedia = new ProductMedia
            {
                Product = product,
                Media = new Media { FileName = fileName, MediaType = MediaType.Image }
            };

            product.AddMedia(productMedia);
        }

        foreach (var productMediaId in variationVm.DeleteNewImages)
        {
            var productMedias = _productMediaRepository.Query().First(x => x.Id == productMediaId);
                _productMediaRepository.Remove(productMedias);
            await _mediaService.DeleteMediaAsync(productMedias.Media);
        }
    }
    private static ProductPriceHistory CreatePriceHistory(User loginUser, Product product)
    {
        return new ProductPriceHistory
        {
            CreatedBy = loginUser,
            Product = product,
            Price = product.Price,
            OldPrice = product.OldPrice,
            SpecialPrice = product.SpecialPrice,
            SpecialPriceStart = product.SpecialPriceStart,
            SpecialPriceEnd = product.SpecialPriceEnd
        };
    }
    private static void MapProductLinkVmToProduct(FormProduct model, Product product)
    {
        foreach (var relatedProductVm in model.Products.RelatedProducts)
        {
            var productLink = new ProductLink
            {
                LinkType = ProductLinkType.Related,
                Product = product,
                LinkedProductId = relatedProductVm.Id
            };

            product.AddProductLinks(productLink);
        }

        foreach (var crossSellProductVm in model.Products.CrossSellProducts)
        {
            var productLink = new ProductLink
            {
                LinkType = ProductLinkType.CrossSell,
                Product = product,
                LinkedProductId = crossSellProductVm.Id
            };

            product.AddProductLinks(productLink);
        }
    }
    private async Task AddOrDeleteProductVariation(User loginUser, FormProduct model, Product product)
    {
        foreach (var productVariationVm in model.Products.Variations)
        {
            var productLink = product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Super).FirstOrDefault(x => x.LinkedProduct.Name == productVariationVm.Name);
            if (productLink == null)
            {
                productLink = new ProductLink
                {
                    LinkType = ProductLinkType.Super,
                    Product = product,
                    LinkedProduct = product.Clone()
                };

                productLink.LinkedProduct.CreatedBy = loginUser.Id;
                productLink.LinkedProduct.EditBy = loginUser.Id;
                productLink.LinkedProduct.Name = productVariationVm.Name;
                productLink.LinkedProduct.Slug = productVariationVm.Name.ToUrlFriendly();
                productLink.LinkedProduct.NormalizedName = productVariationVm.NormalizedName;
                productLink.LinkedProduct.Sku = productVariationVm.Sku;
                productLink.LinkedProduct.Gtin = productVariationVm.Gtin;
                productLink.LinkedProduct.Price = productVariationVm.Price;
                productLink.LinkedProduct.OldPrice = productVariationVm.OldPrice;
                productLink.LinkedProduct.HasOptions = false;
                productLink.LinkedProduct.IsVisibleIndividually = false;
                if (product.Thumbnail != null)
                {
                    productLink.LinkedProduct.Thumbnail = new Media { FileName = product.Thumbnail.FileName };
                }

                await MapProductVariantImageFromVm(productVariationVm, productLink.LinkedProduct);

                foreach (var combinationVm in productVariationVm.OptionCombinations)
                {
                    productLink.LinkedProduct.AddOptionCombination(new ProductOptionCombination
                    {
                        OptionId = combinationVm.OptionId,
                        Value = combinationVm.Value,
                        SortIndex = combinationVm.SortIndex
                    });
                }

                var productPriceHistory = CreatePriceHistory(loginUser, productLink.LinkedProduct);
                productLink.LinkedProduct.PriceHistories.Add(productPriceHistory);
                product.AddProductLinks(productLink);
            }
            else
            {
                var isPriceChanged = false;
                if (productLink.LinkedProduct.Price != productVariationVm.Price ||
                    productLink.LinkedProduct.OldPrice != productVariationVm.OldPrice)
                {
                    isPriceChanged = true;
                }

                productLink.LinkedProduct.EditBy = loginUser.Id;
                productLink.LinkedProduct.EditOn = DateTime.Now;
                productLink.LinkedProduct.Sku = productVariationVm.Sku;
                productLink.LinkedProduct.Name = productVariationVm.Name;
                productLink.LinkedProduct.NormalizedName = productVariationVm.NormalizedName;
                productLink.LinkedProduct.Price = productVariationVm.Price;
                productLink.LinkedProduct.IsDeleted = false;
                productLink.LinkedProduct.StockTrackingIsEnabled = product.StockTrackingIsEnabled;

                await MapProductVariantImageFromVm(productVariationVm, productLink.LinkedProduct);
                if (isPriceChanged)
                {
                    var productPriceHistory = CreatePriceHistory(loginUser, productLink.LinkedProduct);
                    productLink.LinkedProduct.PriceHistories.Add(productPriceHistory);
                }
            }

        }
        foreach (var productLink in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Super))
        {
            if (model.Products.Variations.All(x => x.Name != productLink.LinkedProduct.Name))
            {
                _productLinkRepository.Remove(productLink);
                productLink.LinkedProduct.IsDeleted = true;
            }
        }
    }
    private void AddOrDeleteProductOption(FormProduct model, Product product)
    {
        var optionIndex = 0;
        foreach (var optionVm in model.Products.Options)
        {
            var optionValue = product.OptionValues.FirstOrDefault(x => x.OptionId == optionVm.Id);
            if (optionValue == null)
            {
               product.AddOptionValue(new ProductOptionValue
               {
                   OptionId = optionVm.Id,
                   DisplayType = optionVm.DisplayType,
                   Value = JsonConvert.SerializeObject(optionVm.Values),
                   SortIndex = optionIndex
               });
            }
            else
            {
                optionValue.Value = JsonConvert.SerializeObject(optionVm.Values);
                optionValue.DisplayType = optionVm.DisplayType;
                optionValue.SortIndex = optionIndex;
            }
            optionIndex++;
        }

        var deletedProductOptionValues = product.OptionValues.Where(x => model.Products.Options.All(vm => vm.Id != x.OptionId)).ToList();

        foreach (var productOptionValue in deletedProductOptionValues)
        {
            product.OptionValues.Remove(productOptionValue);
            _productOptionValueRepository.Remove(productOptionValue);
        }
    }
    private void AddOrDeleteCategories(FormProduct model, Product product)
    {
        foreach (var categoryId in model.Products.CategoryIds)
        {
            if (product.Categories.Any(x => x.CategoryId == categoryId))
            {
                continue;
            }

            var productCategory = new ProductCategory
            {
                CategoryId = categoryId
            };
            product.AddCategory(productCategory);
        }
        var deletedProductCategories =
           product.Categories.Where(productCategory => !model.Products.CategoryIds.Contains(productCategory.CategoryId))
               .ToList();

        foreach (var deletedProductCategory in deletedProductCategories)
        {
            deletedProductCategory.Product = null;
            product.Categories.Remove(deletedProductCategory);
            _productCategoryRepository.Remove(deletedProductCategory);
        }
    }
    private void AddOrDeleteProductAttribute(FormProduct model, Product product)
    {
        foreach (var productAttributeVm in model.Products.Attributes)
        {
            var productAttrValue =
                product.AttributeValues.FirstOrDefault(x => x.AttributeId == productAttributeVm.Id);
            if (productAttrValue == null)
            {
                productAttrValue = new ProductAttributeValue
                {
                    AttributeId = productAttributeVm.Id,
                    Value = productAttributeVm.Value
                };
                product.AddAttributeValue(productAttrValue);
            }
            else
            {
                productAttrValue.Value = productAttributeVm.Value;
            }
        }

        var deletedAttrValues =
            product.AttributeValues.Where(attrValue => model.Products.Attributes.All(x => x.Id != attrValue.AttributeId))
                .ToList();

        foreach (var deletedAttrValue in deletedAttrValues)
        {
            deletedAttrValue.Product = null;
            product.AttributeValues.Remove(deletedAttrValue);
            _productAttributeValueRepository.Remove(deletedAttrValue);
        }
    }
    private void AddOrDeleteProductLinks(FormProduct model, Product product)
    {
        foreach (var relatedProductVm in model.Products.RelatedProducts)
        {
            var productLink = product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related).FirstOrDefault(x => x.LinkedProductId == relatedProductVm.Id);
            if (productLink == null)
            {
                productLink = new ProductLink
                {
                    LinkType = ProductLinkType.Related,
                    Product = product,
                    LinkedProductId = relatedProductVm.Id,
                };

                _productLinkRepository.Add(productLink);
            }
        }

        foreach (var productLink in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related))
        {
            if (model.Products.RelatedProducts.All(x => x.Id != productLink.LinkedProductId))
            {
                _productLinkRepository.Remove(productLink);
            }
        }

        foreach (var crossSellProductVm in model.Products.CrossSellProducts)
        {
            var productLink = product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell).FirstOrDefault(x => x.LinkedProductId == crossSellProductVm.Id);
            if (productLink == null)
            {
                productLink = new ProductLink
                {
                    LinkType = ProductLinkType.CrossSell,
                    Product = product,
                    LinkedProductId = crossSellProductVm.Id,
                };

                _productLinkRepository.Add(productLink);
            }
        }

        foreach (var productLink in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell))
        {
            if (model.Products.CrossSellProducts.All(x => x.Id != productLink.LinkedProductId))
            {
                _productLinkRepository.Remove(productLink);
            }
        }
    }
    private void MapUploadedFile(FormProduct model)
    {
        // Currently model binder cannot map the collection of file productImages[0], productImages[1]
        foreach (var file in Request.Form.Files)
        {
            if (file.Name.Contains("Products[variations]"))
            {
                var key = file.Name.Replace("Products", "");
                var keyParts = key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                var variantIndex = int.Parse(keyParts[1]);
                if (key.Contains("NewImages"))
                {
                    model.Products.Variations[variantIndex].NewImages.Add(file);
                }
                if(key.Contains("ThumbnailImage"))
                {
                    model.Products.Variations[variantIndex].ThumbnailImage = file;
                }
            }
        }
    }
    private async Task SaveProductMedias(FormProduct model, Product product)
    {
        if (model.ThumbnailImage != null)
        {
            var fileName = await SaveFile(model.ThumbnailImage);
            if (product.Thumbnail != null)
            {
                product.Thumbnail.FileName = fileName;
            }
            else
            {
                product.Thumbnail = new Media { FileName = fileName };
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
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
        return fileName;
    }

}


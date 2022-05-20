using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class CartRuleController : Controller
{
    public IRepository<CartRule> _cartRuleRepository;
    public CartRuleController(IRepository<CartRule> cartRuleRepository)
    {
        _cartRuleRepository = cartRuleRepository;
    }
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    public IActionResult query()
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
        var cartRule = _cartRuleRepository.Query().AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            cartRule = cartRule.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            cartRule = cartRule.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = cartRule.Count();
        var data = cartRule.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CartRuleForm model)
    {
        if (ModelState.IsValid)
        {
            var cartRule = new CartRule()
            {

                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                StartOn = model.StartOn,
                EndOn = model.EndOn,
                IsCouponRequired = model.IsCouponRequired,
                RuleToApply = model.RuleToApply,
                DiscountAmount = model.DiscountAmount,
                DisCountStep = model.DiscountStep,
                MaxDiscountAmount = model.MaxDiscountAmount,
                UsegeLimitPerCoupon = model.UsageLimitPerCoupon,
                UsegeLimetPerCustomer = model.UsageLimitPerCustomer
            };
            if (model.IsCouponRequired && !string.IsNullOrWhiteSpace(model.CouponCode))
            {
                var coupon = new Coupon
                {
                    CartRule = cartRule,
                    Code = model.CouponCode
                };

                cartRule.Coupons.Add(coupon);
            }

            foreach (var item in model.Products)
            {
                var cartRuleProduct = new CartRuleProduct
                {
                    CartRule = cartRule,
                    ProductId = item.Id
                };
                cartRule.Products.Add(cartRuleProduct);
            }

            _cartRuleRepository.Add(cartRule);
            await _cartRuleRepository.SaveChangesAsync();
            return Ok();
        }
      
        return BadRequest(ModelState);
       
    }

    [HttpGet("{id}")]
    public ActionResult Edit(long id)
    {
        var cartRule = _cartRuleRepository.Query().Include(x => x.Coupons).Include(x => x.Products).ThenInclude(p => p.Product).FirstOrDefault(x => x.Id == id);
        if (cartRule == null)
        {
            return NotFound();
        }
        var cartRuleForm = new CartRuleForm()
        {
            Id = cartRule.Id,
            Name = cartRule.Name,
            Description = cartRule.Description,
            IsActive = cartRule.IsActive,
            StartOn = cartRule.StartOn,
            EndOn = cartRule.EndOn,
            IsCouponRequired = cartRule.IsCouponRequired,
            RuleToApply = cartRule.RuleToApply,
            DiscountAmount = cartRule.DiscountAmount,
            DiscountStep = cartRule.DisCountStep,
            MaxDiscountAmount = cartRule.MaxDiscountAmount,
            UsageLimitPerCoupon = cartRule.UsegeLimitPerCoupon,
            UsageLimitPerCustomer = cartRule.UsegeLimetPerCustomer,
            Products = cartRule.Products.Select(x => new CartRuleProductVm { Id = x.ProductId, Name = x.Product.Name, IsPublished = x.Product.IsPublished }).ToList()
        };

        if (cartRule.IsCouponRequired)
        {
            var coupon = cartRule.Coupons.FirstOrDefault();
            if (coupon != null)
            {
                cartRuleForm.CouponCode = coupon.Code;
            }
        }
        return View(cartRuleForm);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> Edit(long id, CartRuleForm model)
    {
        if (ModelState.IsValid)
        {
            var cartRule = _cartRuleRepository.Query().Include(x => x.Coupons)
                    .Include(x => x.Products).FirstOrDefault(x=> x.Id == id);
            if (cartRule == null)
            {
                return NotFound();
            }
            cartRule.Name = model.Name;
            cartRule.Description = model.Description;
            cartRule.StartOn = model.StartOn;
            cartRule.EndOn = model.EndOn;
            cartRule.IsActive = model.IsActive;
            cartRule.IsCouponRequired = model.IsCouponRequired;
            cartRule.RuleToApply = model.RuleToApply;
            cartRule.DiscountAmount = model.DiscountAmount;
            cartRule.DisCountStep = model.DiscountStep;
            cartRule.MaxDiscountAmount = model.MaxDiscountAmount;
            cartRule.UsegeLimitPerCoupon = model.UsageLimitPerCoupon;
            cartRule.UsegeLimetPerCustomer = model.UsageLimitPerCustomer;


            if (model.IsCouponRequired && !string.IsNullOrWhiteSpace(model.CouponCode))
            {
                var coupon = cartRule.Coupons.FirstOrDefault();
                if (coupon == null)
                {
                    coupon = new Coupon
                    {
                        CartRule = cartRule,
                        Code = model.CouponCode
                    };

                    cartRule.Coupons.Add(coupon);
                }
                else
                {
                    coupon.Code = model.CouponCode;
                }
            }

            foreach (var item in model.Products)
            {
                var cartRuleProduct = cartRule.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (cartRuleProduct == null)
                {
                    cartRuleProduct = new CartRuleProduct
                    {
                        CartRule = cartRule,
                        ProductId = item.Id
                    };
                    cartRule.Products.Add(cartRuleProduct);
                }
            }

            var modelProductIds = model.Products.Select(x => x.Id);
            var deletedProducts = cartRule.Products.Where(x => !modelProductIds.Contains(x.ProductId)).ToList();
            foreach (var item in deletedProducts)
            {
                item.CartRule = null;
                cartRule.Products.Remove(item);
            }

            await _cartRuleRepository.SaveChangesAsync();
            return Ok();
        }
            return BadRequest(ModelState);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(long id)
    {
        var cartRule = await _cartRuleRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
        if (cartRule == null)
        {
            return NotFound();
        }

        try
        {
            _cartRuleRepository.Remove(cartRule);
            await _cartRuleRepository.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { Error = $"The cart rule {cartRule.Name} can't not be deleted because it has been used" });
        }

        return NoContent();
    }
}


namespace Shop.Components;

public class CartBadgeViewComponent : ViewComponent
{
    private readonly ICartService _cartService;
    private readonly IWorkContext _workContext;
    private readonly ICurrencyService _currencyService;

    public CartBadgeViewComponent(ICartService cartService, IWorkContext workContext, ICurrencyService currencyService)
    {
        _cartService = cartService;
        _workContext = workContext;
        _currencyService = currencyService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
        if (cart == null)
        {
            cart = new CartVm(_currencyService);
        }

        return View(cart.Items.Count);
    }
}


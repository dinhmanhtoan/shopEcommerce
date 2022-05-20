﻿namespace Shop.Components;

public class OrderSummaryViewComponent : ViewComponent
{
    private readonly ICartService _cartService;
    private readonly IWorkContext _workContext;
    private readonly ICurrencyService _currencyService;

    public OrderSummaryViewComponent(ICartService cartService, IWorkContext workContext, ICurrencyService currencyService)
    {
        _cartService = cartService;
        _workContext = workContext;
        _currencyService = currencyService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var curentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCartDetails(curentUser.Id);
        if (cart == null)
        {
            cart = new CartVm(_currencyService);
        }
        return View(cart);
    }
}


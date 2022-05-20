﻿namespace Model.Services;

public class FreeShippingServiceProvider : IShippingPriceServiceProvider
{
    private readonly ICurrencyService _currencyService;

    public FreeShippingServiceProvider(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    public Task<GetShippingPriceResponse> GetShippingPrices(GetShippingPriceRequest request, ShippingProvider provider)
    {
        var response = new GetShippingPriceResponse { IsSuccess = true };

        var freeShippingSetting = JsonConvert.DeserializeObject<FreeShippingSetting>(provider.AdditionalSettings);

        if (request.OrderAmount < freeShippingSetting.MinimumOrderAmount)
        {
            return Task.FromResult(response);
        }

        response.ApplicablePrices.Add(new ShippingPrice(_currencyService)
        {
            Name = "Free",
            Price = 0
        });

        return Task.FromResult(response);
    }
}


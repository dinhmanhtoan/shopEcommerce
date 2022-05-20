using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Model.Services;

public class CurrencyService : ICurrencyService
{
    private readonly IConfiguration _config;
    public CultureInfo CurrencyCulture { get; }
    public CurrencyService(IConfiguration config)
    {
        _config = config;
        var currencyCulture = _config.GetValue<string>("Global:CurrencyCulture");
        CurrencyCulture = new CultureInfo(currencyCulture);
    }
    public string FormatCurrency(decimal value)
    {
        var decimalPlace = _config.GetValue<int>("Global:CurrencyDecimalPlace");
        return value.ToString($"C{decimalPlace}", CurrencyCulture);
    }
}


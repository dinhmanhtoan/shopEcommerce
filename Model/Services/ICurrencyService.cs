using System.Globalization;

namespace Model.Services;

public interface ICurrencyService
{
    CultureInfo CurrencyCulture { get; }
    string FormatCurrency(decimal value);
}


namespace Model.Models;
public class ProductOption : EntityBase
{
    public string Name { get; set; }
    public IList<ProductOptionValue> OptionValue { get; set; }
    public IList<ProductOptionCombination> OptionCombinations { get; protected set; } = new List<ProductOptionCombination>();
}


namespace Model.Models;
public class CatalogRuleCustomerGroup
{
    public long CatalogRuleId { get; set; }
    public CatalogRule CatalogRule { get; set; }
    public long CustomerGroupId { get; set; }
    public CustomerGroup CustomerGroup { get; set; }
}


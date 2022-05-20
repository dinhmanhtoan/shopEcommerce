namespace Model.Models;
public class CatalogRule : EntityBase
{
    [Required(ErrorMessage = "This {0} is Not Required")]
    [MaxLength(450)]
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? StartOn { get; set; }
    public DateTimeOffset? EndOn { get; set; }
    public string RuleToApply { get; set; }
    public decimal DisCountAmount { get; set; }
    public decimal MaxDisCountAmount { get; set; }
    public IList<CatalogRuleCustomerGroup> CustomerGroups { get; set; } = new List<CatalogRuleCustomerGroup>();
}


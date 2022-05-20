namespace Model.Models;
public class CartRule : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? StartOn { get; set; }
    public DateTimeOffset? EndOn { get;set; }
    public bool IsCouponRequired { get; set; }
    public string RuleToApply { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal? MaxDiscountAmount { get; set; }
    public int? DisCountStep { get; set; }
    public int? UsegeLimitPerCoupon { get; set; }
    public int? UsegeLimetPerCustomer { get; set; }
    public IList<Coupon> Coupons { get; set; } = new List<Coupon>();
    public IList<CartRuleCustomerGroup> CustomerGroups { get; set; } = new List<CartRuleCustomerGroup>();
    public IList<CartRuleProduct> Products { get; set; } = new List<CartRuleProduct>();
    public IList<CartRuleCategory> Categories { get; set; } = new List<CartRuleCategory>();

}


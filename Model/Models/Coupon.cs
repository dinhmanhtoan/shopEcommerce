namespace Model.Models;
public class Coupon : EntityBase
{
    public Coupon()
    {
        CreateOn = DateTimeOffset.Now;
    }
    public long CartRuleId { get; set; }
    public CartRule CartRule { get; set; }

    [Required(ErrorMessage = "This {0} field is required" )]
    [MaxLength(450)]
    public string Code { get; set; }
    public DateTimeOffset CreateOn { get; set; }

}


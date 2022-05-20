namespace Model.ViewModel.Braintree;
public class BraintreeConfigForm
{
    [Required]
    public string PublicKey { get; set; }

    [Required]
    public string PrivateKey { get; set; }

    [Required]
    public string MerchantId { get; set; }

    [Required]
    public bool IsProduction { get; set; } = false;

    [Newtonsoft.Json.JsonIgnore]
    public string Environment { get { return IsProduction ? "production" : "sandbox"; } }
}

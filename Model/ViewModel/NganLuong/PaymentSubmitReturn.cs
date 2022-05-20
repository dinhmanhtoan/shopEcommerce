using Microsoft.AspNetCore.Mvc;
namespace Model.ViewModel.NganLuong;

public class PaymentSubmitReturn
{
    [FromQuery(Name = "error_code")]
    public string ErrorCode { get; set; }

    [FromQuery(Name = "token")]
    public string Token { get; set; }

    [FromQuery(Name = "order_code")]
    public string OrderCode { get; set; }

    [FromQuery(Name = "order_id")]
    public string OrderId { get; set; }
}


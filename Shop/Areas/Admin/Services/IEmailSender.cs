namespace Shop.Areas.Admin.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
}


namespace BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receiverEmail, string subject, string body);
    }
}
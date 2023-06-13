namespace BLL.Services.Interfaces
{
    public interface IEmailConfirmationService
    {
        Task SendEmailConfirmation(string emailAddress);
        bool VerifyConfirmationCode(string code);
    }
}
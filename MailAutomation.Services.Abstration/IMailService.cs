using MailAutomatioin.Shared;

namespace MailAutomation.Services.Abstration;

public interface IMailService
{
    Task<List<MailExtractModel>> DoMailAutomationAsync(string email, string password);
}

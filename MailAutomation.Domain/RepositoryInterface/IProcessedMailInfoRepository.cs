using MailAutomation.Domain.Entities;

namespace MailAutomation.Domain.RepositoryInterface;

public interface IProcessedMailInfoRepository
{
    Task<ProcessedMailInfo> CreateAsync(ProcessedMailInfo processedMailInfo);
    Task<bool> IsMessageProcessed(string messageID);
}

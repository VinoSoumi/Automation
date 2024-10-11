using MailAutomation.Domain.Entities;
using MailAutomation.Domain.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace MailAutomation.Persistence.Repositories;

internal sealed class ProcessedMailInfoRepository : IProcessedMailInfoRepository
{
    private readonly ApplicationDbContext context;

    public ProcessedMailInfoRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<ProcessedMailInfo> CreateAsync(ProcessedMailInfo processedMailInfo)
    {
        var result = await context.ProcessedMailInfos.AddAsync(processedMailInfo);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            string? message = ex.InnerException?.Message;
            Console.WriteLine(message);
        }
        return result.Entity;
    }

    public async Task<bool> IsMessageProcessed(string messageID)
    {
        ProcessedMailInfo? obj = await context.ProcessedMailInfos.FirstOrDefaultAsync(x => x.MessageID == messageID);
        if(obj == null)
        {
            return false;
        }
        return true;
    }
}

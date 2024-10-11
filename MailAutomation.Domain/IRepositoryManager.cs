using MailAutomation.Domain.RepositoryInterface;

namespace MailAutomation.Domain;

public interface IRepositoryManager
{
    IExpenseRepository ExpenseRepo { get; }
    IProcessedMailInfoRepository ProcessedMailInfoRepo { get; }

    void CreateTransaction();
    void Commit();
    void Rollback();
    void SaveAsync();
}

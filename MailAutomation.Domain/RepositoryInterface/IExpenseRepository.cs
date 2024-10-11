using MailAutomatioin.Shared;
using MailAutomation.Domain.Entities;

namespace MailAutomation.Domain.RepositoryInterface;

public interface IExpenseRepository
{
    Task<IEnumerable<ExpenseList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExpenseList>> GetAllWithMailInfoAsync(CancellationToken cancellationToken = default);
    Task<Expense?> GetByIDAsync(int expenseID, CancellationToken cancellationToken = default);
    Task<Expense> CreateAsync(Expense expense);
    Task<Expense> UpdateAsync(Expense expense);
    Task HardDeleteAsync(int expenseID);
    Task SoftDeleteAsync(int expenseID);
    Task<Expense?> SetActive(int expenseID);
    Task SetInactive(int expenseID);
}

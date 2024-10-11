using MailAutomatioin.Shared;

namespace MailAutomation.Services.Abstration
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseList>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ExpenseList>> GetAllWithMailInfoAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(ExpenseCreate expenseCreate, CancellationToken cancellationToken = default);
    }
}

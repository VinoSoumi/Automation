using MailAutomatioin.Shared;
using MailAutomation.Domain;
using MailAutomation.Domain.Entities;
using MailAutomation.Services.Abstration;

namespace MailAutomation.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepositoryManager repoManager;

        public ExpenseService(IRepositoryManager repoManger)
        {
            this.repoManager = repoManger;
        }
        public async Task CreateAsync(ExpenseCreate expenseCreate, CancellationToken cancellationToken = default)
        {
            try
            {
                if(!await repoManager.ProcessedMailInfoRepo.IsMessageProcessed(expenseCreate.MessageID))
                {
                    repoManager.CreateTransaction();

                    Expense createdExpense = await repoManager.ExpenseRepo.CreateAsync(new Expense
                    {
                        CostCentre = expenseCreate.CostCentre,
                        SalesTaxAmount = expenseCreate.SalesTaxAmount,
                        TaxableAmount = expenseCreate.TaxableAmount,
                        NetAmount = expenseCreate.NetAmount,
                        PaymentMethod = expenseCreate.PaymentMethod,
                        ExtractedContent = expenseCreate.ExtractedContent,
                        CreatedBy = expenseCreate.UserID
                    });

                    await repoManager.ProcessedMailInfoRepo.CreateAsync(new ProcessedMailInfo
                    {
                        MessageID = expenseCreate.MessageID,
                        MessageDate = expenseCreate.MessageDate,
                        SenderName = expenseCreate.SenderName,
                        SenderEmailAddress = expenseCreate.SenderEmailAddress,
                        Subject = expenseCreate.Subject,
                        Body = expenseCreate.Body,
                        CreatedBy = expenseCreate.UserID,
                        ExpenseID = createdExpense.ExpenseID
                    });

                    repoManager.Commit();
                }
            }
            catch (Exception ex)
            {
                repoManager.Rollback();
                //throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ExpenseList>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var expenses = await repoManager.ExpenseRepo.GetAllAsync();
            return expenses;
        }

        public async Task<IEnumerable<ExpenseList>> GetAllWithMailInfoAsync(CancellationToken cancellationToken = default)
        {
            var expenses = await repoManager.ExpenseRepo.GetAllWithMailInfoAsync();
            return expenses;
        }
    }
}

using MailAutomation.Domain;
using MailAutomation.Services.Abstration;

namespace MailAutomation.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IExpenseService> expenseService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            expenseService = new Lazy<IExpenseService>(() => new ExpenseService(repositoryManager));
        }
        public IExpenseService ExpenseService => expenseService.Value;
    }
}

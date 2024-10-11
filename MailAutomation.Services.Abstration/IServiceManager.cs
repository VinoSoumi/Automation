namespace MailAutomation.Services.Abstration;

public interface IServiceManager
{
    IExpenseService ExpenseService { get; }
}

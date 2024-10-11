using MailAutomatioin.Shared;
using MailAutomation.Domain.Entities;
using MailAutomation.Domain.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace MailAutomation.Persistence.Repositories;

internal sealed class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<Expense> CreateAsync(Expense expense)
    {
        var result = await context.Expenses.AddAsync(expense);
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

    public async Task<IEnumerable<ExpenseList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var data = await (from exp in context.Expenses
                    select new ExpenseList
                    {
                        ExpenseID = exp.ExpenseID,
                        CostCentre = exp.CostCentre,
                        SalesTaxAmount = exp.SalesTaxAmount,
                        TaxableAmount = exp.TaxableAmount,
                        NetAmount = exp.NetAmount,
                        PaymentMethod = exp.PaymentMethod,
                        ExtractedContent = exp.ExtractedContent  
                    }).ToListAsync();
        return data;
    }

    public async Task<IEnumerable<ExpenseList>> GetAllWithMailInfoAsync(CancellationToken cancellationToken = default)
    {
        var data = await (from exp in context.Expenses
                          join mInfo in context.ProcessedMailInfos
                          on exp.ExpenseID equals mInfo.ExpenseID
                          select new ExpenseList
                          {
                              ExpenseID = exp.ExpenseID,
                              CostCentre = exp.CostCentre,
                              SalesTaxAmount = exp.SalesTaxAmount,
                              TaxableAmount = exp.TaxableAmount,
                              NetAmount = exp.NetAmount,
                              PaymentMethod = exp.PaymentMethod,
                              ExtractedContent = exp.ExtractedContent,
                              MailInfo = new()
                              {
                                  MessageID = mInfo.MessageID,
                                  MessageDate = mInfo.MessageDate,
                                  SenderName = mInfo.SenderName,
                                  SenderEmailAddress = mInfo.SenderEmailAddress,
                                  Subject = mInfo.Subject,
                                  Body = mInfo.Body
                              }  
                          }).ToListAsync();
        return data;
    }

    public Task<Expense?> GetByIDAsync(int expenseID, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task HardDeleteAsync(int expenseID)
    {
        throw new NotImplementedException();
    }

    public Task<Expense?> SetActive(int expenseID)
    {
        throw new NotImplementedException();
    }

    public Task SetInactive(int expenseID)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(int expenseID)
    {
        throw new NotImplementedException();
    }

    public Task<Expense> UpdateAsync(Expense expense)
    {
        throw new NotImplementedException();
    }
}

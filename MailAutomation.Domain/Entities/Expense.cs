namespace MailAutomation.Domain.Entities;

public class Expense : BaseEntity
{
    public int ExpenseID { get; set; }
    public string CostCentre { get; set; }
    public double SalesTaxAmount { get; set; }
    public double TaxableAmount { get; set; }
    public double NetAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string ExtractedContent { get; set; }

    public ProcessedMailInfo MailInfo { get; set; }
}

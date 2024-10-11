namespace MailAutomatioin.Shared;

public class ExpenseCreate
{
    public int ExpenseID { get; set; }
    public string CostCentre { get; set; }
    public double SalesTaxAmount { get; set; }
    public double TaxableAmount { get; set; }
    public double NetAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string ExtractedContent { get; set; }
    public int UserID { get; set; }

    public string MessageID { get; set; }
    public DateTime MessageDate { get; set; }
    public string SenderName { get; set; }
    public string SenderEmailAddress { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

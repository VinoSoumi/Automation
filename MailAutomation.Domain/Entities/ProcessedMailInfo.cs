namespace MailAutomation.Domain.Entities
{
    public class ProcessedMailInfo : BaseEntity
    {
        public int ProcessedMailInfoID { get; set; }
        public string MessageID { get; set; }
        public DateTime MessageDate { get; set; }
        public string SenderName { get; set; }
        public string SenderEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public int ExpenseID { get; set; }
        public Expense Expense { get; set; }

    }
}

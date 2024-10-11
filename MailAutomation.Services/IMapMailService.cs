using MailAutomation.Services.Abstration;
using MailKit.Search;
using MailKit.Security;
using System.Xml.Serialization;
using System.Xml;
using MailKit.Net.Imap;
using MailAutomatioin.Shared;
using MailAutomation.Domain.Entities;
using MailAutomation.Domain.Exceptions;
using MailAutomation.Domain;

namespace MailAutomation.Services;

public class IMapMailService : IMailService
{
    private readonly IServiceManager serviceManager;
    private double taxRate = 18;
    private int userID = 1001;
    private const string host = "imap.gmail.com";
    private const int port = 993;
    public IMapMailService(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }
    public async Task<List<MailExtractModel>> DoMailAutomationAsync(string email, string password)
    {
        var expenses = new List<MailExtractModel>();
        using (var client = new ImapClient())
        {
            await client.ConnectAsync(host, port, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(email, password);
            var inbox = client.Inbox;
            await inbox.OpenAsync(MailKit.FolderAccess.ReadOnly);

            var query = SearchQuery.BodyContains("<expense><cost_centre>")
                                   .And(SearchQuery.BodyContains("<total>"))
                                   .And(SearchQuery.BodyContains("<payment_method>"));
            var results = await inbox.SearchAsync(query);

            foreach (var uniqueId in results)
            {
                var message = await inbox.GetMessageAsync(uniqueId);
                var fromAddresses = message.From;
                string senderName = fromAddresses[0].Name;
                string senderEmailAddress = fromAddresses[0].ToString().Split('<', '>')[1];
                string extracted = ExtractMailContent(message.GetTextBody(MimeKit.Text.TextFormat.Text).Replace(Environment.NewLine, " "), "<expense>", "</expense>");

                try
                {
                    new XmlDocument().LoadXml(extracted);
                }
                catch (Exception e)
                {
                    continue;
                }

                MailExtractModel expense;
                XmlSerializer serializer = new XmlSerializer(typeof(MailExtractModel));
                using (StringReader reader = new StringReader(extracted))
                {
                    expense = (MailExtractModel)serializer.Deserialize(reader);
                }

                if (string.IsNullOrEmpty(expense.Total))
                {
                    continue;
                }

                expense.CostCentre = string.IsNullOrEmpty(expense.CostCentre) ? "UNKNOWN" : expense.CostCentre;
                expense.ExtractedContent = extracted;
                expense.MessageID = message.MessageId;
                expense.MessageDate = message.Date.DateTime;
                expense.SenderName = senderName;
                expense.SenderEmailAddress = senderEmailAddress;
                expense.Subject = message.Subject;
                expense.Body = message.TextBody;

                await CreateExpense(expense);

                expenses.Add(expense);
            }

            await client.DisconnectAsync(true);
        }
        return expenses;
    }

    private async Task CreateExpense(MailExtractModel expense)
    {
        double netAmount = Convert.ToDouble(expense.Total);
        double taxAmount = Math.Round((netAmount * taxRate) / (100 + taxRate));
        double taxableAmount = netAmount - taxAmount;

        await serviceManager.ExpenseService.CreateAsync(new ExpenseCreate
        {
            CostCentre = expense.CostCentre,
            TaxableAmount = taxableAmount,
            SalesTaxAmount = taxAmount,
            NetAmount = netAmount,
            PaymentMethod = expense.PaymentMethod,
            ExtractedContent = expense.ExtractedContent,
            UserID = userID,
            MessageID = expense.MessageID,
            MessageDate = expense.MessageDate,
            SenderName = expense.SenderName,
            SenderEmailAddress = expense.SenderEmailAddress,
            Subject = expense.Subject,
            Body = expense.Body  
        });
    }

    private static string ExtractMailContent(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0);
            End = strSource.IndexOf(strEnd, Start);
            int len = strEnd.Length;
            int calc = End + len;
            string result = strSource.Substring(Start, calc - Start);
            return result;
        }
        return "";
    }
}

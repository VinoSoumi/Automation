using System.Xml.Serialization;

namespace MailAutomatioin.Shared
{
    [XmlRoot(ElementName = "expense")]
    public class MailExtractModel
    {
        [XmlElement(ElementName = "cost_centre")]
        public string CostCentre { get; set; }

        [XmlElement(ElementName = "total")]
        public string Total { get; set; }

        [XmlElement(ElementName = "payment_method")]
        public string PaymentMethod { get; set; }

        public string ExtractedContent { get; set; }
        public string MessageID { get; set; }
        public DateTime MessageDate { get; set; }
        public string SenderName { get; set; }
        public string SenderEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

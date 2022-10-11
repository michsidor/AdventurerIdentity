namespace AdventurerOfficialProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? userMessage { get; set; }

        public DateTime AddData { get; set; }

        public string SenderName { get; set; }
        public string RecipientName { get; set; }   
    }
}

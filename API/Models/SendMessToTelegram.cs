namespace API.Models
{
    public class SendMessToTelegram
    {
        public class SendMessRequest
        {
            public string SenderName { get; set; }
            public string From { get; set; }
            public string Content { get; set; }
        }

    }
}

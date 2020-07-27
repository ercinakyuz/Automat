namespace Automat.Infrastructure.ExceptionHandling.Contracts
{
    public class MessageContract
    {
        public string Code { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageType Type { get; set; }
    }
}
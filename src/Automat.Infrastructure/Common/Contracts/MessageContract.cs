using Automat.Infrastructure.ExceptionHandling.Contracts;

namespace Automat.Infrastructure.Common.Contracts
{
    public class MessageContract
    {
        public string Code { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageType Type { get; set; }
    }
}
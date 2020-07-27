using System.Collections.Generic;

namespace Automat.Infrastructure.Common.Contracts
{
    public abstract class CommandResultBase
    {
        public ValidationState ValidationState { get; set; }
        public IEnumerable<MessageContract> Messages { get; set; }
    }
}
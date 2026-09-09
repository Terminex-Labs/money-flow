using Terminex.Common.Results;

namespace Shared.Kernel.Exceptions
{
    public class DomainException : Exception
    {
        public Error Error { get; init; }

        public DomainException(Error error) : base(error.Message) => Error = error;

        public DomainException(Error error, Exception inner) : base(error.Message, inner) => Error = error;
    }
}

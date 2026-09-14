using Terminex.Common.Results;

namespace Shared.Kernel.Exceptions
{
    public sealed class InvalidIdentifierException(Error error) : DomainException(error);
}
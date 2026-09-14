using Terminex.Common.Results;

namespace Shared.Kernel.Exceptions
{
    public sealed class StringEmptyException(Error error) : DomainException(error);
}
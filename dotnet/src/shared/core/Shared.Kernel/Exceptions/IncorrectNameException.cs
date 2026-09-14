using Terminex.Common.Results;

namespace Shared.Kernel.Exceptions
{
    public sealed class IncorrectNameException(Error error) : DomainException(error);
}
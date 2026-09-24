using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.All
{
    public sealed class GetAllTypeAccountHandler(ITypeAccountReadOnlyRepository repository) : IRequestHandler<GetAllTypeAccountQuery, Result<List<TypeAccountResponse>>>
    {
        public async Task<Result<List<TypeAccountResponse>>> Handle(GetAllTypeAccountQuery request, CancellationToken cancellationToken)
        {
            var typeAccounts = await repository.GetAllAsync(cancellationToken);

            return typeAccounts;
        }
    }
}
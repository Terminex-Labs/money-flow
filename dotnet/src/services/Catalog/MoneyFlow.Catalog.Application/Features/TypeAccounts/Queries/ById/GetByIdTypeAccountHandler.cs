using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.ById
{
    public sealed class GetByIdTypeAccountHandler(ITypeAccountReadOnlyRepository repository) : IRequestHandler<GetByIdTypeAccountQuery, Result<TypeAccountResponse>>
    {
        public async Task<Result<TypeAccountResponse>> Handle(GetByIdTypeAccountQuery request, CancellationToken cancellationToken)
        {
            var typeAccount = await repository.GetByIdAsync(request.Id);

            if (typeAccount == null)
                return Error.NotFound("Тип счета не найден!");
            
            return typeAccount;
        }
    }
}
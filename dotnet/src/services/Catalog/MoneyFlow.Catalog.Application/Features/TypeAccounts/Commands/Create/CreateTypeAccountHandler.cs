using MediatR;
using Terminex.Common.Results;
using MoneyFlow.Catalog.Domain.Models;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Create
{
    public sealed class CreateTypeAccountHandler(ITypeAccountRepository repository, IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateTypeAccountCommand, Result<CreatedTypeAccountResponse>>
    {
        public async Task<Result<CreatedTypeAccountResponse>> Handle(CreateTypeAccountCommand request, CancellationToken cancellationToken)
        {
            var typeAccount = TypeAccount.Create(TypeAccountName.Create(request.Name));

            await repository.AddAsync(typeAccount, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedTypeAccountResponse(typeAccount.Id);
        }
    }
}
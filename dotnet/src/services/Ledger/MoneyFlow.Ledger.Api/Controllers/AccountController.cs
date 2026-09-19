using MediatR;
using Shared.Api;
using Shared.Api.Extensions;
using Terminex.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Ledger.Contracts.Accounts.Request;
using MoneyFlow.Ledger.Application.Features.Accounts.Queries.All;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Create;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Delete;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Freeze;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Unfreeze;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.UpdateName;

namespace MoneyFlow.Ledger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/account")]
    public sealed class AccountController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request, CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var command = new CreateAccountCommand(extractResult.Value.UserId, request.Name, Guid.Parse(request.TypeAccountId), Guid.Parse(request.CurrencyId), request.Balance, request.IsActive);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var query = new GetAllAccountQuery(extractResult.Value.UserId);

            var result = await mediator.Send(query, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch("name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateAccountNameRequest request, CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var command = new UpdateAccountNameCommand(extractResult.Value.UserId, Guid.Parse(request.Id), request.Name);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch("freeze/{id}")]
        public async Task<IActionResult> Freeze([FromRoute] Guid id, CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var command = new FreezeAccountCommand(extractResult.Value.UserId, id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch("unfreeze/{id}")]
        public async Task<IActionResult> Unfreeze([FromRoute] Guid id, CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var command = new UnfreezeAccountCommand(extractResult.Value.UserId, id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            Result<ExtractData> extractResult = this.CredentialsAccessData(User);

            if (extractResult.IsFailure)
                return extractResult.Value.ActionResult;

            var command = new DeleteAccountCommand(extractResult.Value.UserId, id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }
    }
}
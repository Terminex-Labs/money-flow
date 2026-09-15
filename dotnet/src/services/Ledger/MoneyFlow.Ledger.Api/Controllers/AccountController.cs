using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Ledger.Contracts.Accounts.Request;
using MoneyFlow.Ledger.Application.Features.Accounts.Queries.All;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Create;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Freeze;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.Unfreeze;
using MoneyFlow.Ledger.Application.Features.Accounts.Commands.UpdateName;

namespace MoneyFlow.Ledger.Api.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public sealed class AccountController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request, CancellationToken ct = default)
        {
            var command = new CreateAccountCommand(request.Name, Guid.Parse(request.TypeAccountId), Guid.Parse(request.CurrencyId), request.Balance, request.IsActive);

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
            var query = new GetAllAccountQuery();

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
            var command = new UpdateAccountNameCommand(Guid.Parse(request.Id), request.Name);

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
            var command = new FreezeAccountCommand(id);

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
            var command = new UnfreezeAccountCommand(id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }
    }
}
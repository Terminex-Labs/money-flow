using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Catalog.Api.Constants;
using Shared.Catalog.Contracts.Request;
using Microsoft.AspNetCore.Authorization;
using MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.All;
using MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.ById;
using MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Create;
using MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Delete;
using MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Update;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/type/transaction")]
    public sealed class TypeTransactionController(IMediator mediator) : Controller
    {
        [HttpPost]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Create([FromBody] CreateTypeTransactionRequest request, CancellationToken ct = default)
        {
            var command = new CreateTypeTransactionCommand(request.Name);

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
            var query = new GetAllTypeTransactionQuery();

            var result = await mediator.Send(query, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var query = new GetByIdTypeTransactionQuery(id);

            var result = await mediator.Send(query, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Update([FromBody] UpdateTypeTransactionRequest request, CancellationToken ct = default)
        {
            var command = new UpdateTypeTransactionCommand(Guid.Parse(request.Id), request.Name);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpDelete("{id}")]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Update([FromRoute] Guid id, CancellationToken ct = default)
        {
            var command = new DeleteTypeTransactionCommand(id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }
    }
}
using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Catalog.Contracts.Request;
using MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.All;
using MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Create;
using MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Update;
using MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Delete;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/type/account")]
    public class TypeAccountController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTypeAccountRequest request, CancellationToken ct = default)
        {
            var command = new CreateTypeAccountCommand(request.Name);

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
            var query = new GetAllTypeAccountQuery();

            var result = await mediator.Send(query, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateTypeAccountRequest request, CancellationToken ct = default)
        {
            var command = new UpdateTypeAccountCommand(Guid.Parse(request.Id), request.Name);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            var command = new DeleteTypeAccountCommand(id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }
    }
}
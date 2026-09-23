using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Catalog.Api.Constants;
using Shared.Catalog.Contracts.Request;
using Microsoft.AspNetCore.Authorization;
using MoneyFlow.Catalog.Application.Features.Currencies.Queries.All;
using MoneyFlow.Catalog.Application.Features.Currencies.Commands.Create;
using MoneyFlow.Catalog.Application.Features.Currencies.Commands.Update;
using MoneyFlow.Catalog.Application.Features.Currencies.Commands.Delete;
using MoneyFlow.Catalog.Application.Features.Currencies.Queries.ById;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/currency")]
    public sealed class CurrencyController(IMediator mediator) : Controller
    {
        [HttpPost]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyRequest request, CancellationToken ct = default)
        {
            var command = new CreateCurrencyCommand(request.ShortName, request.Unicode, request.FullName);

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
            var query = new GetAllCurrencyQuery();

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
            var query = new GetByIdCurrencyQuery(id);

            var result = await mediator.Send(query, ct);

            return result.Match
            (
                onSuccess: () => Ok(result.Value),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpPatch]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Update([FromBody] UpdateCurrencyRequest request, CancellationToken ct = default)
        {
            var command = new UpdateCurrencyCommand(Guid.Parse(request.Id), request.ShortName, request.Unicode, request.FullName);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }

        [HttpDelete("{id}")]
        [Authorize(AuthorizationPolicyConstants.ADMIN_ONLY)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            var command = new DeleteCurrencyCommand(id);

            var result = await mediator.Send(command, ct);

            return result.Match
            (
                onSuccess: () => Ok(),
                onFailure: error => this.MapActionResult(error)
            );
        }
    }
}
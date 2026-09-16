using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Catalog.Contracts.Request;
using MoneyFlow.Catalog.Application.Features.Currencies.Queries.All;
using MoneyFlow.Catalog.Application.Features.Currencies.Commands.Create;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/currency")]
    internal sealed class CurrencyController(IMediator mediator) : Controller
    {
        [HttpPost]
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
    }
}
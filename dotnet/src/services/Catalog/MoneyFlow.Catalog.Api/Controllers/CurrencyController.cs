using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Catalog.Application.Features.Currencies.Queries.All;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/currency")]
    internal sealed class CurrencyController(IMediator mediator) : Controller
    {
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
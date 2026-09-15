using MediatR;
using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.All;

namespace MoneyFlow.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/type/account")]
    public class TypeAccountController(IMediator mediator) : Controller
    {
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
    }
}
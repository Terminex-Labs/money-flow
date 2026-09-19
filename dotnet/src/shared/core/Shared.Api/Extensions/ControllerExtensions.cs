using System.Security.Claims;
using Terminex.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static Result<ExtractData> CredentialsAccessData(this Controller controller, ClaimsPrincipal principal)
        {
            var extractData = new ExtractData();

            string? userIdString = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdString))
            {
                extractData.ActionResult = controller.Unauthorized("Не был найден `UserId`!");
                return extractData;
            }

            if (!Guid.TryParse(userIdString, out Guid userIdGuid))
            {
                extractData.ActionResult = controller.BadRequest("Не верный формат `UserId`!");
                return extractData;
            }

            extractData.UserId = userIdGuid;
            
            return extractData;
        }
    }
}
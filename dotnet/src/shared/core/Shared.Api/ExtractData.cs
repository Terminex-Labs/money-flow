using Microsoft.AspNetCore.Mvc;

namespace Shared.Api
{
    public sealed class ExtractData()
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; } = null!;
        public IActionResult ActionResult { get; set; } = null!;
    }
}
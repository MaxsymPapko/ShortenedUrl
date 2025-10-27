using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Data;

namespace UrlShortener.Api.Controllers
{
    [Route("")]
    public class RedirectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RedirectController(ApplicationDbContext db) => _db = db;

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var item = await _db.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
            if (item == null) return NotFound("Short code not found.");
            return Redirect(item.OriginalUrl);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Data;
using UrlShortener.Api.Models;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortUrlsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShortUrlsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET api/shorturls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.ShortUrls
                .Include(s => s.CreatedBy)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return Ok(list);
        }

        // GET api/shorturls/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.ShortUrls.Include(s => s.CreatedBy).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST api/shorturls
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShortUrlDto dto)
        {
            if (!Uri.IsWellFormedUriString(dto.OriginalUrl, UriKind.Absolute))
                return BadRequest("Invalid URL");

            var exists = await _db.ShortUrls.FirstOrDefaultAsync(x => x.OriginalUrl == dto.OriginalUrl);
            if (exists != null) return Conflict("This URL already exists.");

            string code;
            do
            {
                code = ShortCodeGenerator.Generate(6);
            } while (await _db.ShortUrls.AnyAsync(x => x.ShortCode == code));

            var user = await _userManager.GetUserAsync(User)!;

            var item = new ShortUrl
            {
                OriginalUrl = dto.OriginalUrl,
                ShortCode = code,
                CreatedById = user.Id
            };

            _db.ShortUrls.Add(item);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // DELETE api/shorturls/{id}
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.ShortUrls.FindAsync(id);
            if (item == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = user != null && (await _userManager.IsInRoleAsync(user, "Admin"));

            if (!isAdmin && item.CreatedById != user?.Id)
                return Forbid();

            _db.ShortUrls.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }


    }



    public class CreateShortUrlDto
    {
        public string OriginalUrl { get; set; } = null!;
    }
}

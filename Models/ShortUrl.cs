using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Api.Models
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OriginalUrl { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ShortedUrl { get; set; } = null!;

        [Required]
        public string CreatedById { get; set; } = null!;
        public ApplicationUser? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

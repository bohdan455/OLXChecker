using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OlxSubscribeModel
    {
        [EmailAddress]
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        [Url]
        [Required]
        [StringLength(500)]
        public string Url { get; set; } = string.Empty;
    }
}

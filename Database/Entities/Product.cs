using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Url { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string LastPrice { get; set; } = string.Empty;
        public ICollection<Email> Emails { get; set; }
    }
}

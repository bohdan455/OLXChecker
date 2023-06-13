using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public int ProductId { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
    }
}

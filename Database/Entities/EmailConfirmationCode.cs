using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class EmailConfirmationCode
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Email Email { get; set; }
    }
}

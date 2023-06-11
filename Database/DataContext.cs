using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DataContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebUniform.Models;

namespace WebUniform.Data
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
          : base(options)
        {
        }

        public DbSet<Slack> Slacks { get; set; }
        public DbSet<Uniform> Uniforms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
      
    }
}

using Microsoft.EntityFrameworkCore;

namespace ottplatform.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Information> Information { get; set; }

        public DbSet<Information2> Information2 { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<SecondAdmin> SecondAdmin { get; set; }
    }
}


using Microsoft.EntityFrameworkCore;
using TryitterAPI.Models;

namespace TryitterAPI.Repository
{
    public class TryitterContext : DbContext
    {
        public TryitterContext() { }
        public TryitterContext(DbContextOptions<TryitterContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Post> Post { get; set; }
    }

}

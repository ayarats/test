using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RepositoryContext : DbContext
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"DataSource=testDB.db;");
            DbInitializer.Seed(this);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments);
        }
    }
}

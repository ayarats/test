using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebService.Interfaces;

namespace Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext() { }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}

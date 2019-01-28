using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebService.Interfaces;

namespace Data
{
    public class Repository : IRepository
    {
        private readonly RepositoryContext _context;

        public Repository(RepositoryContext context)
        {
            _context = context;
        }

        public IQueryable<Post> GetAllPosts()
        {
            return _context.Posts.Include(x => x.Comments).AsQueryable();
        }

        public async Task<Post> GetPost(string id)
        {
            return await _context.FindAsync<Post>(id);
        }

        public async Task<T> Add<T>(T entity) where T : Message
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var post = await _context.FindAsync<Post>(id);
            if (post == null)
            {
                return false;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                post.Comments?.ForEach(x => _context.Remove(x));
                _context.Remove(post);
                if (_context.SaveChanges() > 0)
                {
                    transaction.Commit();
                    return true;
                }
                return false;
            }
        }
    }
}

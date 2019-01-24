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
            try
            {
                return _context.Posts.Include(x => x.Comments).AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Post> GetPost(string id)
        {
            try
            {
                var post = await _context.FindAsync<Post>(id);
                return post;
            }
            catch
            {
                return null;
            }
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task Delete(string id)
        {
            var post = await _context.FindAsync<Post>(id);
            if (post == null)
            {
                return;
            }
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    post.Comments?.ForEach(x => _context.Remove(x.Id));
                    _context.Remove(post);
                    if (_context.SaveChanges() > 0)
                    {
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

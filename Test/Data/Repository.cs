using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
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
            return _context.Posts.AsQueryable();
        }

        public async Task<Post> GetPost(string id)
        {
            var post = await _context.FindAsync<Post>(id);
            return post;
        }

        public async Task AddPost(Post post)
        {
            try
            {
                await _context.AddAsync(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task AddComment(Comment comment)
        {
            try
            {
                await _context.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            await Task.CompletedTask;
        }

        public async Task Delete(string id)
        {
            var post = await GetPost(id);
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
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

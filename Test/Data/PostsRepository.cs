using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebService.Interfaces;

namespace Data
{
    public class PostsRepository : IPostsRepository
    {
        private readonly RepositoryContext _context;

        public PostsRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPost(string id)
        {
            return await _context.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> AddPost(Post post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePost(string id)
        {
            var post = await _context.FindAsync<Post>(id);
            if (post == null)
            {
                return false;
            }
            _context.Remove(post);
            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
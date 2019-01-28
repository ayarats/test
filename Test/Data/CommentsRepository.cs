using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.Interfaces;

namespace Data
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly RepositoryContext _context;

        public CommentsRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetComment(string id)
        {
            return await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Comment> GetAllComments()
        {
            return _context.Comments.AsNoTracking().AsQueryable();
        }

        public IQueryable<Comment> GetComments(int number, int page)
        {
            return _context.Comments.AsNoTracking().Skip(number * page).Take(number).AsQueryable();
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}

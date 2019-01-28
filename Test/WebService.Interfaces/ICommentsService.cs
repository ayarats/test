using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Interfaces
{
    public interface ICommentsService
    {
        Task<Comment> GetComment(string id);
        IQueryable<Comment> GetComments(int number, int page);
        IQueryable<Comment> GetAllComments();
        Task<Comment> AddComment(Comment comment);
    }
}

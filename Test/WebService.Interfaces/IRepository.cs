using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Interfaces
{
    public interface IRepository
    {
        Task<Post> GetPost(string id);
        IQueryable<Post> GetAllPosts();
        Task AddPost(Post post);
        Task AddComment(Comment comment);
        Task Delete(string id);
    }
}

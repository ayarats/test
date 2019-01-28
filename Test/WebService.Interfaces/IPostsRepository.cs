using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Interfaces
{
    public interface IPostsRepository
    {
        Task<Post> GetPost(string id);
        Task<bool> DeletePost(string id);
        Task<Post> AddPost(Post post);
    }
}

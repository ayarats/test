using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Interfaces
{
    public interface IPostsService
    {
        Task<Post> GetPost(string id);
        Task<Post> AddPost(Post post);
        Task<bool> DeletePost(string id);
    }
}

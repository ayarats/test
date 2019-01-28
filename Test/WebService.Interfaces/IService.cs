using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;

namespace WebService.Interfaces
{
    public interface IService
    {
        Task<Post> GetPost(string id);
        Task<Post> AddPost(Post post);
        Task<Comment> AddComment(Comment comment);
        Task<bool> Delete(string id);
    }
}

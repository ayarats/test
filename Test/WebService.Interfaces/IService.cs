using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;

namespace WebService.Interfaces
{
    public interface IService
    {
        Task<Post> GetPost(string id);
        Task<HttpStatusCode> AddPost(Post post);
        Task<HttpStatusCode> AddComment(Comment comment);
        Task<HttpStatusCode> Delete(string id);
    }
}

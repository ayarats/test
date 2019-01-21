using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;

namespace WebService.Interfaces
{
    public interface IRepository
    {
        Task<Post> GetPost(string id);
        Task AddPost(Post post);
        Task AddComment(Comment comment);
        Task Delete(string id);
    }
}

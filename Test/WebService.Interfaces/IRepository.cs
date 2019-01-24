using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Interfaces
{
    public interface IRepository
    {
        Task<Post> GetPost(string id);
        IQueryable<Post> GetAllPosts();
        Task Delete(string id);
        Task<T> Add<T>(T entity) where T : class;
    }
}

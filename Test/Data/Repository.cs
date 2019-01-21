using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;
using WebService.Interfaces;

namespace Data
{
    public class Repository : IRepository
    {
        private RepositoryContext Context { get; set; }

        public Repository()
        {
            Context = new RepositoryContext();
        }

        public async Task<Post> GetPost(string id)
        {
            await Task.CompletedTask;

            var post = await Context.FindAsync<Post>(id);

            throw new System.NotImplementedException();
        }

        public async Task AddPost(Post post)
        {
            await Task.CompletedTask;
            throw new System.NotImplementedException();
        }

        public async Task AddComment(Comment comment)
        {
            await Task.CompletedTask;
            throw new System.NotImplementedException();
        }

        public async Task Delete(string id)
        {
            await Task.CompletedTask;
            throw new System.NotImplementedException();
        }
    }
}

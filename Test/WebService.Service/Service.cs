using System.Net;
using System.Threading.Tasks;
using Domain;
using WebService.Interfaces;

namespace WebService.Service
{
    public class Service : IService
    {
        public IRepository Repository { get; set; }

        public Service(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<Post> GetPost(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var post = new Post();
            try
            {
                post = await Repository.GetPost(id);
            }
            catch
            {
                return null;
            }
            return post;
        }

        public async Task<HttpStatusCode> AddComment(Comment comment)
        {
            if (comment == null || comment.Post == null || string.IsNullOrWhiteSpace(comment.Text))
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await Repository.Add(comment);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await Repository.Delete(id);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> AddPost(Post post)
        {
            if (post == null || string.IsNullOrWhiteSpace(post.Text))
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await Repository.Add(post);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}

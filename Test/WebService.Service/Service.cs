using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;
using WebService.Interfaces;

namespace WebService.Service
{
    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            _repository = repository;
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
                post = await _repository.GetPost(id);
            }
            catch
            {
                return null;
            }
            return post;
        }

        public async Task<HttpStatusCode> AddComment(Comment comment)
        {
            if (comment == null)
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await _repository.AddComment(comment);
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
                await _repository.Delete(id);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> AddPost(Post post)
        {
            if (post == null)
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await _repository.AddPost(post);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}

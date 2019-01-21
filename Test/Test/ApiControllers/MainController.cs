using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebService.Converters;
using WebService.Interfaces;
using WebService.Models;

namespace WebService.ApiControllers
{
    [System.Web.Http.Route("api/[controller]")]
    public class MainController : ApiController
    {
        private readonly IService _service;

        public MainController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<PostModel> GetPost(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var domainPost = await _service.GetPost(id);
            return domainPost.ToView();
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddPost(PostModel post)
        {
            if (post == null)
            {
                return HttpStatusCode.BadRequest;
            }
            return await _service.AddPost(post.ToDomain());
        }

        [System.Web.Http.HttpPost]
        public async Task<HttpStatusCode> AddComment(CommentModel comment)
        {
            if (comment == null)
            {
                return HttpStatusCode.BadRequest;
            }
            return await _service.AddComment(comment.ToDomain());
        }

        [HttpDelete]
        public async Task<HttpStatusCode> DeletePost(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                return HttpStatusCode.BadRequest;
            }
            return await _service.Delete(postId);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebService.Interfaces;

namespace WebService.Service
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _repository;

        public PostsService(IPostsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Post> GetPost(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }
            return await _repository.GetPost(id);
        }

        public async Task<bool> DeletePost(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }
            return await _repository.DeletePost(id);
        }

        public async Task<Post> AddPost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "Post cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(post.Text))
            {
                throw new ArgumentNullException(nameof(post.Text), "Text cannot be null.");
            }
            return await _repository.AddPost(post);
        }
    }
}

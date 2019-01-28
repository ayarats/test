using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebService.Interfaces;

namespace WebService.Service
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _repository;
        public CommentsService(ICommentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Comment> GetComment(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }

            return await _repository.GetComment(id);
        }

        public IQueryable<Comment> GetComments(int number, int page)
        {
            if (number == 0)
            {
                throw new ArgumentNullException(nameof(number), "Number cannot be null.");
            }
            return _repository.GetComments(number, page);
        }

        public IQueryable<Comment> GetAllComments()
        {
            return _repository.GetAllComments();
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentException(nameof(comment));
            }
            if (comment.Post == null)
            {
                throw new ArgumentNullException(nameof(comment.Post), "Post cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(comment.Text))
            {
                throw new ArgumentNullException(nameof(comment.Text), "Text cannot be null.");
            }
            return await _repository.AddComment(comment);
        }
    }
}

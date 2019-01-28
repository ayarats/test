using System;
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
                throw new ArgumentNullException();
            }
            try
            {
                return await Repository.GetPost(id);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            if (comment == null || comment.Post == null || string.IsNullOrWhiteSpace(comment.Text))
            {
                throw new ArgumentException();
            }
            try
            {
                return await Repository.Add(comment);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException();
            }
            try
            {
                return await Repository.Delete(id);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<Post> AddPost(Post post)
        {
            if (post == null || string.IsNullOrWhiteSpace(post.Text))
            {
                throw new ArgumentNullException();
            }
            try
            {
                return await Repository.Add(post);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}

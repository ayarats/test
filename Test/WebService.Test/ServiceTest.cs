using System.Linq;
using System.Net;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    [Trait("Service", "All")]
    public class ServiceTest
    {
        private readonly RepositoryContext _context;
        private readonly IRepository _repository;
        private readonly IService _service;
        
        public ServiceTest()
        {
            _context = new RepositoryContext();
            _repository = new Repository(_context);
            _service = new Service.Service(_repository);
        }

        [Theory]
        [InlineData("asd 0")]
        [InlineData("asd 2")]
        [InlineData("asd 1")]

        public async void AddPost_ShouldPass(string text)
        {
            RemoveAllPosts();
            var post = new Post { Text = text };
            var result = await _service.AddPost(post);
            var added = _repository.GetAllPosts().First(x => x.Text.Equals(text));
            Assert.Equal(HttpStatusCode.OK, result);
            Assert.NotNull(added.Id);
            RemoveAllPosts();
        }


        [Fact]
        public async void AddPost_ShouldNotPass()
        {
            RemoveAllPosts();

            var post = new Post();

            var add = await _service.AddPost(post);

            Assert.Equal(HttpStatusCode.BadRequest, add);
        }

        [Fact]
        public async void AddComment_ShouldPass()
        {
            RemoveAllPosts();
            var test = _repository.GetAllPosts();
            var post = new Post { Text = "Your add may be placed here!" };
            await _service.AddPost(post);
            var addedPost = _repository.GetAllPosts().First(x => x.Text.Equals("Your add may be placed here!"));

            var comment = new Comment { Text = "This is comment", Post = addedPost };

            var result = await _service.AddComment(comment);
            var newPost = await _repository.GetPost(addedPost.Id);

            Assert.Equal(HttpStatusCode.OK, result);
            Assert.Equal("This is comment", newPost.Comments.First().Text);
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData("asd")]
        [InlineData("zxc")]
        [InlineData("Remove me")]
        public async void RemovePost_ShouldPass(string text)
        {
            RemoveAllPosts();

            var post = new Post {Text = text};
            await _repository.Add(post);

            var added = _repository.GetAllPosts().First();
            await _service.Delete(added.Id);

            var find = await _repository.GetPost(id);

            Assert.Null(find);


            RemoveAllPosts();
        }

        private void RemoveAllPosts()
        {
            var posts = _repository.GetAllPosts().ToList();
            posts.ForEach(x => _repository.Delete(x.Id));
        }
    }
}

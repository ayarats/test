using System;
using System.Linq;
using System.Net;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    //[Trait("Service", "All")]
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
            var result = await _service.AddPost(new Post { Text = text });
            Assert.NotNull(result.Id);
            RemoveAllPosts();
        }


        [Fact]
        public async void AddPost_ShouldNotPass()
        {
            RemoveAllPosts();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.AddPost(new Post()));
        }

        [Fact]
        public async void AddComment_ShouldPass()
        {
            RemoveAllPosts();
            var post = new Post { Text = "Your add may be placed here!" };
            
            var addedPost = await _service.AddPost(post);

            var result = await _service.AddComment(new Comment { Text = "This is comment", Post = addedPost });

            Assert.NotNull(result.Id);
            Assert.Equal("This is comment", addedPost.Comments.First().Text);

            RemoveAllPosts();
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData("asd")]
        [InlineData("zxc")]
        [InlineData("Remove me")]
        public async void RemovePost_ShouldPass(string text)
        {
            RemoveAllPosts();

            var post = await _repository.Add(new Post { Text = text });

            var result = await _service.Delete(post.Id);

            Assert.True(result);


            RemoveAllPosts();
        }

        [Fact]
        public async void RemovePost_ShouldNotPass()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.Delete(null));
        }

        [Theory]
        [InlineData("text test", "comment")]
        public async void GetPost_ShouldPass(string text,string commentText)
        {
            RemoveAllPosts();

            var post = await _service.AddPost(new Post {Text = text});
            await _service.AddComment(new Comment {Post = post, Text = commentText });
            var addedPost = await _service.GetPost(post.Id);

            Assert.NotNull(addedPost.Id);
            Assert.NotNull(addedPost.Comments.First().Id);
            Assert.Equal(text, addedPost.Text);
            Assert.Equal(commentText, addedPost.Comments.First().Text);
            
            RemoveAllPosts();
        }

        [Fact]
        public async void GetPost_ShouldNotPass()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(()=> _service.GetPost(null));
        }

        private void RemoveAllPosts()
        {
            var posts = _repository.GetAllPosts().ToList();
            posts.ForEach(x => _repository.Delete(x.Id));
        }
    }
}

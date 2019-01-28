using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    public class PostsServiceTests
    {
        private readonly Mock<RepositoryContext> _context;
        private readonly Mock<IPostsRepository> _repository;
        private readonly Mock<DbSet<Post>> _mockSet;

        private readonly IPostsService _service;

        public PostsServiceTests()
        {
            IQueryable<Post> posts = new List<Post>().AsQueryable();

            _mockSet = new Mock<DbSet<Post>>();
            _mockSet.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(posts.Provider);
            _mockSet.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(posts.Expression);
            _mockSet.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(posts.ElementType);
            _mockSet.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(posts.GetEnumerator());

            _context = new Mock<RepositoryContext>();
            _context.Setup(x => x.Posts).Returns(_mockSet.Object);
            _repository = new Mock<IPostsRepository>();
            _service = new Service.PostsService(_repository.Object);
        }

        [Theory]
        [InlineData("asd 0")]
        [InlineData("asd 2")]
        [InlineData("asd 1")]

        public async void AddPost_ShouldPass(string text)
        {
            var post = new Post { Text = text };
            await _service.AddPost(post);

            _repository.Verify(x => x.AddPost(post), Times.Once);
        }


        [Fact]
        public async void AddPost_ShouldNotPass()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.AddPost(new Post()));
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData("asd")]
        [InlineData("zxc")]
        [InlineData("Remove me")]
        public async void RemovePost_ShouldPass(string id)
        {
            await _service.DeletePost(id);
            _repository.Verify(x => x.DeletePost(id));
        }

        [Fact]
        public async void RemovePost_ShouldNotPass()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.DeletePost(null));
        }

        [Theory]
        [InlineData("id")]
        public async void GetPost_ShouldPass(string id)
        {
            await _service.GetPost(id);
            _repository.Verify(x=>x.GetPost(id));
        }
    }
}

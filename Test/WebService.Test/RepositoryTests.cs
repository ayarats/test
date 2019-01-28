using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    public class RepositoryTests
    {
        private readonly RepositoryContext _context;
        private readonly IPostsRepository _repository;

        private readonly Post _post = new Post{Id = "TestId", Text = "text"};
        
        public RepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName:"InMemoryDb")
                .Options;
            _context = new RepositoryContext(dbOptions);
            _context.Posts.Add(_post);
            _context.SaveChanges();
            _repository = new PostsRepository(_context);
        }

        [Theory]
        [InlineData("test 1")]
        [InlineData("test 2")]
        public async void AddPost_ShouldPass(string text)
        {
            var post = await _repository.AddPost(new Post {Text = text});

            Assert.NotNull(post.Id);
            Assert.Equal(text, post.Text);
        }

        [Theory]
        [InlineData("TestId")]
        public async void Delete_ShouldPass(string id)
        {
            var delete = await _repository.DeletePost(id);
            Assert.True(delete);
        }

        [Fact]
        public async void Delete_ShouldNotPass()
        {
            var delete = await _repository.DeletePost(null);

            Assert.False(delete);
        }
    }
}

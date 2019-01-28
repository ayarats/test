using System;
using System.Linq;
using Data;
using Domain;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    public class RepositoryTests
    {
        private readonly RepositoryContext _context;
        private readonly IRepository _repository;
        
        public RepositoryTests()
        {
            _context = new RepositoryContext();
            _repository = new Repository(_context);
        }

        [Theory]
        [InlineData("test 1")]
        [InlineData("test 2")]
        public async void AddPost_ShouldPass(string text)
        {
            RemoveAllPosts();

            var post = await _repository.Add<Post>(new Post {Text = text});


            Assert.NotNull(post.Id);
            Assert.Equal(text, post.Text);

            RemoveAllPosts();
        }

        [Theory]
        [InlineData("test", "post text")]
        public async void AddPostWithComment_ShouldPass(string text, string commentPost)
        {
            RemoveAllPosts();

            var post = await _repository.Add(new Post { Text = text });
            await _repository.Add(new Comment { Post = post, Text = commentPost });
            var delete = await _repository.Delete(post.Id);

            Assert.NotNull(post.Id);
            Assert.True(delete);

            RemoveAllPosts();
        }

        [Theory]
        [InlineData("test post", "test comment 1")]
        public async void AddComment_ShouldPass(string postText, string commentText)
        {
            RemoveAllPosts();
            var post = await _repository.Add(new Post { Text = postText });
            var firstComments = post.Comments?.ToList();

            await _repository.Add(new Comment { Post = post, Text = commentText });
            var secondComments = post.Comments.First();

            Assert.Null(firstComments);
            Assert.Equal(commentText, secondComments.Text);

            RemoveAllPosts();
        }

        [Fact]
        public async void Delete_ShouldNotPass()
        {
            var delete = await _repository.Delete(null);

            Assert.False(delete);
        }

        private void RemoveAllPosts()
        {
            var posts = _repository.GetAllPosts().ToList();
            posts.ForEach(x => _repository.Delete(x.Id));
        }
    }
}

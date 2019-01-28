using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    public class CommentsRepositoryTests
    {
        private readonly RepositoryContext _context;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsRepository _postrepository;
        private readonly int _commentsNumber = 13;
        private readonly Post _post1 = new Post
        {
            Text = "text",
            Comments = new List<Comment>
                {
                    new Comment() {Text = "1"},
                    new Comment() {Text = "2"},
                    new Comment() {Text = "3"},
                    new Comment() {Text = "4"},
                    new Comment() {Text = "5"},
                    new Comment() {Text = "6"},
                    new Comment() {Text = "7"},
                    new Comment() {Text = "8"},
                    new Comment() {Text = "9"},
                    new Comment() {Text = "10"},
                    new Comment() {Text = "11"},
                    new Comment() {Text = "12"}
                }
        };
        private readonly Post _post2 = new Post
        {
            Text = "text",
            Comments = new List<Comment>
                {
                    new Comment() {Id = "Id1",Text = "1"}
                }
        };

        public CommentsRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;
            _context = new RepositoryContext(dbOptions);
            _context.Posts.Add(_post1);
            _context.Posts.Add(_post2);
            _context.SaveChanges();
            _commentsRepository = new CommentsRepository(_context);
            _postrepository = new PostsRepository(_context);
        }

        [Theory]
        [InlineData("test post", "test comment 1")]
        public async void AddComment_ShouldPass(string postText, string commentText)
        {
            var post = await _postrepository.AddPost(new Post { Text = postText });
            var firstComments = post.Comments?.ToList();

            await _commentsRepository.AddComment(new Comment { Post = post, Text = commentText });
            var secondComments = post.Comments.First();

            Assert.Null(firstComments);
            Assert.Equal(commentText, secondComments.Text);
        }

        [Theory]
        [InlineData("Id1", "1")]
        public async void GetPost_ShouldPass(string id, string expectedText)
        {
            var comment = await _commentsRepository.GetComment(id);

            Assert.Equal(expectedText, comment.Text);
        }

        [Theory]
        [InlineData(2, 2)]
        public void GetComments(int number, int pages)
        {
            var comments = _commentsRepository.GetComments(number, pages);
            var texts = comments.Select(x => x.Text).ToList();
            Assert.Equal(number, comments.Count());
            Assert.Contains("5", texts);
            Assert.Contains("6", texts);
        }

        [Fact]
        public void GetAllComments_ShouldPass()
        {
            var comments = _commentsRepository.GetAllComments();
            Assert.Equal(_commentsNumber, comments.Count());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebService.Interfaces;
using Xunit;

namespace WebService.Test
{
    public class CommentsServiceTests
    {

        private readonly Mock<RepositoryContext> _context;
        private readonly Mock<ICommentsRepository> _repository;
        private readonly Mock<DbSet<Comment>> _mockSet;

        private readonly ICommentsService _service;

        public CommentsServiceTests()
        {
            IQueryable<Comment> comments = new List<Comment>().AsQueryable();

            _mockSet = new Mock<DbSet<Comment>>();
            _mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(comments.Provider);
            _mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(comments.Expression);
            _mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(comments.ElementType);
            _mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(comments.GetEnumerator());

            _context = new Mock<RepositoryContext>();
            _context.Setup(x => x.Comments).Returns(_mockSet.Object);
            _repository = new Mock<ICommentsRepository>();
            _service = new Service.CommentsService(_repository.Object);
        }

        [Fact]
        public async void AddComment_ShouldPass()
        {
            var comment = new Comment { Text = "comment", Post = new Post { Text = "post" } };
            await _service.AddComment(comment);
            _repository.Verify(x => x.AddComment(comment), Times.Once);
        }

        [Theory]
        [MemberData(nameof(WrongComments))]
        public async void AddComment_ShouldNotPass(string commentText, string postText, string postId)
        {
            var post = new Post();
            if (postText == null) post = null;
            else
            {
                post.Text = postText; post.Id = postId;
            }
            var comment = new Comment{Text = commentText, Post = post};

            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.AddComment(comment));
            _repository.Verify(x=>x.AddComment(comment), Times.Never);
        }

        [Theory]
        [InlineData(1, 1)]
        public void GetComments_ShouldPass(int number, int page)
        {
            _service.GetComments(number, page);
            _repository.Verify(x => x.GetComments(number, page), Times.Once);
        }

        [Fact]
        public void GetAllComments_ShouldPass()
        {
            _service.GetAllComments();
            _repository.Verify(x => x.GetAllComments(), Times.Once);
        }

        [Fact]
        public void GetComments_ShouldNotPass()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => _service.GetComments(0, 1));
            _repository.Verify(x => x.GetComments(0, 1), Times.Never);
        }

        public static IEnumerable<object[]> WrongComments => new List<object[]>
        {
            new object[] {null, "post", "postId"},
            new object[] { "comment", null, null}
        };
    }



}

using Domain;
using WebService.Models;

namespace WebService.Converters
{
    public static class CommentConverter
    {
        public static CommentModel ToView(this Comment domainComment)
        {
            return new CommentModel
            {
                CommentId = domainComment.Id,
                Text = domainComment.Text,
                PostId = domainComment.Post.Id
            };
        }

        public static Comment ToDomain(this CommentModel viewModel)
        {
            return new Comment
            {
                Id = viewModel.CommentId,
                Text = viewModel.Text
            };
        }
    }
}

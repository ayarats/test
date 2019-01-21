using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebService.Models;

namespace WebService.Converters
{
    public static class PostConverter
    {
        public static PostModel ToView(this Post domainPost)
        {
            return new PostModel
            {
                Id = domainPost.Id,
                Text = domainPost.Text,
                Comments = domainPost.Comments.Select(x=>new CommentModel{CommentId = x.Id, Text = x.Text, PostId = domainPost.Id}).ToList()
            };
        }

        public static Post ToDomain(this PostModel viewModel)
        {
            return new Post
            {
                Id = viewModel.Id,
                Text = viewModel.Text
            };
        }
    }
}

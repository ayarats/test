using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}

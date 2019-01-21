using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebService.Models
{
    public class CommentModel
    {
        public string CommentId { get; set; }
        public string Text { get; set; }
        public string PostId { get; set; }
    }
}

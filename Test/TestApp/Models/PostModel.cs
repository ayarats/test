using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Text { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}

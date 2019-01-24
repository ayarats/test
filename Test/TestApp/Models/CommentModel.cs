using System.ComponentModel.DataAnnotations;
using Domain;

namespace TestApp.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Text { get; set; }
        [Required]
        public Post Post { get; set; }
    }
}

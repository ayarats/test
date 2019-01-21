using System.Collections.Generic;

namespace Domain
{
    public class Post : Message
    {
        public virtual List<Comment> Comments { get; set; }
    }
}
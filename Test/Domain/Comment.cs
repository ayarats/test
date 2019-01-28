namespace Domain
{
    public class Comment : Message
    {
        public virtual Post Post { get; set; }
    }
}
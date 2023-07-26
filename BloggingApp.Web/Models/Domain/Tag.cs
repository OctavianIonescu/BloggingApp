namespace BloggingApp.Web.Models.Domain
{
    public class Tag
    {
        public Guid ID { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public ICollection<BlogPost> blogPosts { get; set; }
    }
}

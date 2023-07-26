using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Web.Models.Domain
{
    public class BlogPost
    {
        [Column]
        public Guid ID { get; set; }
        [Column]
        public string Author { get; set; }
        [Column]
        public string heading { get; set; }
        [Column]
        public string pageTitle { get; set; }
        [Column]
        public string content { get; set; }
        [Column]
        public string shortDescription { get; set; }
        [Column]
        public string featuredURL { get; set; }
        [Column]
        public string URLHandle { get; set; }
        [Column]
        public DateTime publishedDate { get; set; }
        [Column]
        public bool visible { get; set; }
        [Column]
        public ICollection<Tag> Tags { get; set; }
        public ICollection<BlogPostComment> BlogPostComments { get; set; }
        public ICollection<BlogPostLike> BlogPostLikes { get; set; }
        public BlogPost() { }

    }
}

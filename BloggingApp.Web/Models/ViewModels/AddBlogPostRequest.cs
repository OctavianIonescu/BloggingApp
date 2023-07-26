using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggingApp.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public Guid ID { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedURL { get; set; }
        public string URLHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }


        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
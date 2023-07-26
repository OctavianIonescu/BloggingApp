using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggingApp.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid ID { get; set; }
        public string heading { get; set; }
        public string pageTitle { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public string featuredURL { get; set; }
        public string URLHandle { get; set; }
        public string shortDescription { get; set; }
        public DateTime publishedDate { get; set; }
        public bool visible { get; set; }
        public IEnumerable<SelectListItem> tags { get; set; }
        public string[] SelectedTags { get; set; }
    }
}
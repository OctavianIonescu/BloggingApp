using BloggingApp.Web.Models.Domain;
using BloggingApp.Web.Models.ViewModels;
using BloggingApp.Web.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;

namespace BloggingApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepo tagRepo;
        private readonly IBlogPostRepo blogPostRepo;

        public AdminBlogPostController(ITagRepo tagRepo, IBlogPostRepo blogPostRepo)
        {
            this.tagRepo = tagRepo;
            this.blogPostRepo = blogPostRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepo.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.name, Value = x.ID.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest req)
        {
            var newPost = new BlogPost
            {
                heading = req.Heading,
                pageTitle = req.PageTitle,
                Author = req.Author,
                publishedDate = req.PublishedDate,
                shortDescription = req.ShortDescription,
                featuredURL = req.FeaturedURL,
                visible = req.Visible,
                content = req.Content,
                URLHandle = req.URLHandle
            };
            var tags = new List<Tag>();
            foreach (var item in req.SelectedTags)
            {
                var itemGuid = Guid.Parse(item);
                var existingTag = await tagRepo.GetAsync(itemGuid);
                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
            }
            newPost.Tags = tags;
            await blogPostRepo.AddAsync(newPost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var posts = await blogPostRepo.GetAllAsync();
            return View(posts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
            var post = await blogPostRepo.GetAsync(ID);
            var tagList = await tagRepo.GetAllAsync();
            if (post != null)
            {
                var model = new EditBlogPostRequest
                {
                    ID = post.ID,
                    heading = post.heading,
                    pageTitle = post.pageTitle,
                    content = post.content,
                    author = post.Author,
                    featuredURL = post.featuredURL,
                    URLHandle = post.URLHandle,
                    shortDescription = post.shortDescription,
                    publishedDate = post.publishedDate,
                    visible = post.visible,
                    tags = tagList.Select(t => new SelectListItem { Text = t.name, Value = t.ID.ToString() }),
                    SelectedTags = post.Tags.Select(x => x.ID.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest req)
        {
            var newPost = new BlogPost
            {
                ID = req.ID,
                heading = req.heading,
                pageTitle = req.pageTitle,
                Author = req.author,
                content = req.content,
                shortDescription = req.shortDescription,
                featuredURL = req.featuredURL,
                publishedDate = req.publishedDate,
                URLHandle = req.URLHandle,
                visible = req.visible,
            };
            var tags = new List<Tag>();
            foreach (var item in req.SelectedTags)
            {
                var itemGuid = Guid.Parse(item);
                var existingTag = await tagRepo.GetAsync(itemGuid);
                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
            }
            newPost.Tags = tags;
            var updateBlog = await blogPostRepo.UpdateAsync(newPost);
            if (updateBlog != null)
            {
                //success
                return RedirectToAction("List");
            }
            //failure
            return RedirectToAction("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest req)
        {
            var delPost = await blogPostRepo.DeleteAsync(req.ID);
            if (delPost != null)
            {
                //success
                return RedirectToAction("List");
            }
            //failure
            return RedirectToAction("Edit", new { id = req.ID });
        }
    }
}

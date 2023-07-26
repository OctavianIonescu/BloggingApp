using BloggingApp.Web.Repos;
using BloggingApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BloggingApp.Web.Models.Domain;
using BloggingApp.Web.Models.ViewModels;

namespace BloggingApp.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepo blogPostRepo;
        private readonly IBlogPostLikeRepo blogPostLikeRepo;
        private readonly IBlogPostCommentRepo blogPostCommentRepo;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostRepo blogPostRepo, IBlogPostLikeRepo blogPostLikeRepo, IBlogPostCommentRepo blogPostCommentRepo, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.blogPostRepo = blogPostRepo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.blogPostCommentRepo = blogPostCommentRepo;
            this.blogPostLikeRepo = blogPostLikeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(String urlHandle)
        {
            var liked = false;
            var blogPost = await blogPostRepo.GetByURLAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();
            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepo.GetTotalLikes(blogPost.ID);
                if (signInManager.IsSignedIn(User))
                {
                    //Is blog liked by user?
                    var likes = await blogPostLikeRepo.GetLikesForBlog(blogPost.ID);
                    var userID = userManager.GetUserId(User);
                    if(userID != null)
                    {
                        var likeUser = likes.FirstOrDefault(x => x.UserID == Guid.Parse(userID));
                        liked = likeUser != null;
                    }
                }

                //Get Comments
                var blogComments = await blogPostCommentRepo.GetCommentsByBlogIdAsync(blogPost.ID);
                var blogCommentsList = new List<BlogPostCommentView>();
                foreach (var item in blogComments)
                {
                    blogCommentsList.Add(new BlogPostCommentView
                    {
                        Description = item.Description,
                        DateAdded = item.DateAdded,
                        UserName = (await userManager.FindByIdAsync(item.UserID.ToString())).UserName
                    });
                }

                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.ID,
                    Content = blogPost.content,
                    PageTitle = blogPost.pageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.featuredURL,
                    Heading = blogPost.heading,
                    PublishedDate = blogPost.publishedDate,
                    ShortDescription = blogPost.shortDescription,
                    UrlHandle = blogPost.URLHandle,
                    Visible = blogPost.visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = blogCommentsList
                };
            }
            return View(blogDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domain = new BlogPostComment
                {
                    BlogPostID = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserID = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await blogPostCommentRepo.AddAsync(domain);
                return RedirectToAction("Index", "Blogs", new {UrlHandle = blogDetailsViewModel.UrlHandle});
            }
            return View();
        }
    }
}

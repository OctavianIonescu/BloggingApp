using BloggingApp.Web.Models.Domain;
using BloggingApp.Web.Models.ViewModels;
using BloggingApp.Web.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : Controller
    {       private readonly IBlogPostLikeRepo blogPostLikeRepo;

            public BlogPostLikeController(IBlogPostLikeRepo blogPostLikeRepository)
            {
                this.blogPostLikeRepo = blogPostLikeRepository;
            }


            [HttpPost]
            [Route("Add")]
            public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
            {
                var model = new BlogPostLike
                {
                    BlogPostID = addLikeRequest.BlogPostId,
                    UserID = addLikeRequest.UserId
                };

                await blogPostLikeRepo.AddLikeForBlog(model);

                return Ok();
            }


            [HttpGet]
            [Route("{blogPostId:Guid}/totalLikes")]
            public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
            {
                var totalLikes = await blogPostLikeRepo.GetTotalLikes(blogPostId);

                return Ok(totalLikes);
            }
    }
}

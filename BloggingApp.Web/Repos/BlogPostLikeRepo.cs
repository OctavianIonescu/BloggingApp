using BloggingApp.Web.Data;
using BloggingApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Repos
{
    public class BlogPostLikeRepo : IBlogPostLikeRepo
    {
        private readonly BloggingAppDbContext context;

        public BlogPostLikeRepo(BloggingAppDbContext context)
        {
            this.context = context;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await context.BlogPostLike.AddAsync(blogPostLike);
            await context.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await context.BlogPostLike.Where(x => x.BlogPostID == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await context.BlogPostLike.CountAsync(x => x.BlogPostID == blogPostId);
        }
    }
}

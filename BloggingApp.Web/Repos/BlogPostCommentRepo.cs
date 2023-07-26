using BloggingApp.Web.Data;
using BloggingApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Repos
{
    public class BlogPostCommentRepo : IBlogPostCommentRepo
    {
        private readonly BloggingAppDbContext context;
        public BlogPostCommentRepo(BloggingAppDbContext context)
        {
            this.context = context;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await context.BlogPostComment.AddAsync(blogPostComment);
            await context.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await context.BlogPostComment.Where(x => x.BlogPostID == blogPostId).ToListAsync();
        }
    }
}

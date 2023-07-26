using BloggingApp.Web.Models.Domain;

namespace BloggingApp.Web.Repos
{
    public interface IBlogPostCommentRepo
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
    }
}

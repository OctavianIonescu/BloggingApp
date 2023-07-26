using BloggingApp.Web.Models.Domain;

namespace BloggingApp.Web.Repos
{
    public interface IBlogPostLikeRepo
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}

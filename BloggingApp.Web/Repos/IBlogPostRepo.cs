using BloggingApp.Web.Models.Domain;

namespace BloggingApp.Web.Repos
{
    public interface IBlogPostRepo
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetAsync(Guid ID);
        Task<BlogPost> GetByURLAsync(string URLHandle);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost> UpdateAsync(BlogPost post);
        Task<BlogPost> DeleteAsync(Guid ID);
    }
}

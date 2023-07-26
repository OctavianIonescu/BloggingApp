using BloggingApp.Web.Data;
using BloggingApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace BloggingApp.Web.Repos
{
    public class BlogPostRepo : IBlogPostRepo
    {
        private readonly BloggingAppDbContext context;
        public BlogPostRepo(BloggingAppDbContext context)
        {
            this.context = context;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await context.AddAsync(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost> DeleteAsync(Guid ID)
        {
            var target = await context.BlogPosts.FindAsync(ID);
            if (target != null)
            {
                context.BlogPosts.Remove(target);
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await context.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid ID)
        {
            return await context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<BlogPost> GetByURLAsync(string URLHandle)
        {
            return await context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.URLHandle == URLHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost post)
        {
            var target = await context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(y => y.ID == post.ID);
            if (target != null)
            {
                target.ID = post.ID;
                target.heading = post.heading;
                target.pageTitle = post.pageTitle;
                target.content = post.content;
                target.shortDescription = post.shortDescription;
                target.Author = post.Author;
                target.featuredURL = post.featuredURL;
                target.URLHandle = post.URLHandle;
                target.visible = post.visible;
                target.publishedDate = post.publishedDate;
                target.Tags = post.Tags;
                await context.SaveChangesAsync();
                return target;
            }
            return null;
        }
    }
}

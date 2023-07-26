using BloggingApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace BloggingApp.Web.Data
{
    public class BloggingAppDbContext : DbContext
    {
        public BloggingAppDbContext(DbContextOptions<BloggingAppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }

}

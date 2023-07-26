using BloggingApp.Web.Data;
using BloggingApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Repos
{
    public class TagRepo : ITagRepo
    {
        private readonly BloggingAppDbContext _context;
        public TagRepo(BloggingAppDbContext context)
        {
            this._context = context;
        }
        async Task<Tag> ITagRepo.AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        async Task<Tag?> ITagRepo.DeleteAsync(Guid id)
        {
            var targetTag = await _context.Tags.FindAsync(id);
            if (targetTag != null)
            {
                _context.Tags.Remove(targetTag);
                await _context.SaveChangesAsync();
                return targetTag;
            }
            return null;
        }

        async Task<IEnumerable<Tag>> ITagRepo.GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        Task<Tag?> ITagRepo.GetAsync(Guid id)
        {
            return _context.Tags.FirstOrDefaultAsync(t => t.ID == id);
        }

        async Task<Tag?> ITagRepo.UpdateAsync(Tag tag)
        {
            var targetTag = await _context.Tags.FindAsync(tag.ID);
            if (targetTag != null)
            {
                targetTag.name = tag.name;
                targetTag.displayName = tag.displayName;
                await _context.SaveChangesAsync();
                return targetTag;
            }
            return null;
        }
    }
}

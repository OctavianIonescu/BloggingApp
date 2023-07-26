using BloggingApp.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly AuthDbContext context;
        public UserRepo(AuthDbContext context)
        {
            this.context = context;
        }
        public  async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await context.Users.ToListAsync();
            var superAdminUser = await context.Users.FirstOrDefaultAsync(x=> x.Email == "superadmin@yahoo.com");

            if(superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace BloggingApp.Web.Repos
{
    public interface IUserRepo
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
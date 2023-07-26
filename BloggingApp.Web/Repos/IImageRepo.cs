namespace BloggingApp.Web.Repos
{
    public interface IImageRepo
    {
        Task<string> UploadAsync(IFormFile file);
    }
}

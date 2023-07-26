using BloggingApp.Web.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BloggingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepo imageRespository;

        public ImagesController(IImageRepo imageRespository)
        {
            this.imageRespository = imageRespository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var imageURL = await imageRespository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Sometihng went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}

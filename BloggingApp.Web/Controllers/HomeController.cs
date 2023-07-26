using BloggingApp.Web.Models;
using BloggingApp.Web.Models.ViewModels;
using BloggingApp.Web.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloggingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepo blogPostRepo;
        private readonly ITagRepo tagRepo;

        public HomeController(ILogger<HomeController> logger,
            IBlogPostRepo blogPostRepository,
            ITagRepo tagRepository
            )
        {
            _logger = logger;
            this.blogPostRepo = blogPostRepository;
            this.tagRepo = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            // getting all blogs
            var blogPosts = await blogPostRepo.GetAllAsync();

            // get all tags
            var tags = await tagRepo.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


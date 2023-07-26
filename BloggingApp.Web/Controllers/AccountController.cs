using BloggingApp.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel req)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = req.Username,
                    Email = req.Email
                };
                var identityResult = await userManager.CreateAsync(identityUser, req.Password);

                if (identityResult.Succeeded)
                {
                    var roleIDR = await userManager.AddToRoleAsync(identityUser, "User");
                    if(roleIDR.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnURL)
        {
            var model = new LoginViewModel();
            model.ReturnURL = returnURL;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signInRes = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (signInRes.Succeeded && signInRes != null )
            {
                if(!string.IsNullOrWhiteSpace(model.ReturnURL))
                {
                    return Redirect(model.ReturnURL);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TodoMinimalWebApp.Data.Repositories;
using TodoMinimalWebApp.ViewModels;

namespace TodoMinimalWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IAccountRepository _repo { get; }

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = new RegisterUserViewModel
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName
                };
                var result = await _repo.SignUpUserAsync(userModel);
                if (result)
                {
                    return RedirectToAction("login");
                }
            }
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                // login activity -> cookie [Roles and Claims]
                var result = await _repo.SignInUserAsync(userViewModel);
                //login cookie and transfter to the client 
                if (result is not null)
                {
                    // add token to session 
                    HttpContext.Session.SetString("JWToken", result);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "invalid login credentials");
            }
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}

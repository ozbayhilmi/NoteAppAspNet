using Deneme6.Models;
using Deneme6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deneme6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAction _userAction;

        public HomeController(IUserAction userAction)
        {
            _userAction = userAction;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string mail, string password)
        {
            var user = _userAction.ListUsers().FirstOrDefault(u => u.MailAddress == mail && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserMail", user.MailAddress);
                if (user.IsAdmin)
                {
                    return RedirectToAction("AdminMenu", "Admin");
                }
                return RedirectToAction("UserMenu", "User");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kayıtlı değil.");
            }
            
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {
            user.IsActive = true;
            var users = _userAction.ListUsers();
            if (!users.Any())
            {
                user.IsAdmin = true;
            }
            else
            {
                user.IsAdmin = false;
            }
            _userAction.AddUser(user);
            return RedirectToAction("Login");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

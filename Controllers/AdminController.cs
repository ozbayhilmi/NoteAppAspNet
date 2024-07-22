using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deneme6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deneme6.Controllers
{
    public class AdminController : Controller
    {
        private IUserAction _userAction;

        public AdminController(IUserAction userAction)
        {
            _userAction = userAction;
        }

        public IActionResult AdminMenu()
        {
            return View();
        }

        public IActionResult ListUsers()
        {
            var users = _userAction.ListUsers();
            return View(users);
        }

        public IActionResult DeactivateUser(int userId)
        {
            _userAction.DeactivateUser(userId);
            return RedirectToAction("ListUsers");
        }
    }

}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Web.Models.ViewModels;

namespace SimpleCRUD.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            var ListOfUsers = new UsersViewModel
           {
                Users = _userManager.Users.ToList()
        };

            return View("Index", ListOfUsers);
        }
    }
}

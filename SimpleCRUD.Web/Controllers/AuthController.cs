using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Web.Models.ViewModels;


namespace SimpleCRUD.Web.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model, string ReturnUrl)

        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            //var result = await _signInManager.SignInAsync(user, isPersistent:false);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                if (!string.IsNullOrEmpty(ReturnUrl)&& Url.IsLocalUrl(ReturnUrl))
                {
                    // return LocalRedirect(ReturnUrl);
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "People");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            
        }



        //Check if email exists in DB
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email) {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already used");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    //Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index", "People");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
               
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Redirect to a page after successful logout (e.g., home page)
            return RedirectToAction("Index", "Home");
        }

   
    }

}


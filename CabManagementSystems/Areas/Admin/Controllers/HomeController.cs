using CabManagementSystems.Data;
using CabManagementSystems.Models;
using CabManagementSystems.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CabManagementSystems.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class HomeController
        : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        

        public HomeController(ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        //[Route("[area]/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[Route("[area]/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid details.");
                return View(model);
            }

            var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (res.Succeeded && model.Email == "admin@admin.com")
            {
                //return RedirectToAction("Index", "Home", new {Area=""});
                return Redirect("/");
            }
            ModelState.AddModelError("", "Invalid email / password");
            return View(model);
        }

    }
}

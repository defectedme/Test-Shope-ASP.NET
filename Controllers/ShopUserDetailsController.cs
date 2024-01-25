using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;
using System.Security.Claims;
using Test_Shope_ASP.NET.Context.Services;
using Microsoft.IdentityModel.Tokens;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopUserDetailsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TestShopDbContext _context;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private readonly IShopUserDetailsServices _services;




        public ShopUserDetailsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TestShopDbContext context, IShopUserDetailsServices services)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _services = services;
        }

        public async Task<IActionResult> Edit()
        {


            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "ShopeCategory");
            }
            var user = await GetCurrentUserAsync();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {

            if (!ModelState.IsValid) return View(applicationUser);

            var user = await GetCurrentUserAsync();
            await _services.Update(applicationUser);
            return View(user);
        }
    }
}

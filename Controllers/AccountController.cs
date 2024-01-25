using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;
using Test_Shope_ASP.NET.Models.BusinessLogic;

namespace Test_Shope_ASP.NET.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TestShopDbContext _context;



        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TestShopDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;


        }




        [HttpGet]
        public IActionResult Users()
        {

            var users =  _userManager.Users;  
            return View(users);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login userLoginDate)
        {



            if (!ModelState.IsValid)
            {
                return View(userLoginDate);
            }
            // Validate the credentials (dummy example)
            await _signInManager.PasswordSignInAsync(userLoginDate.UserName, userLoginDate.Password, true, true);


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register userRegisterDate)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDate);
            }
            //register logic
            var newUser = new ApplicationUser
            {
                FullName = userRegisterDate.FullName,
                Email = userRegisterDate.Email,
                UserName = userRegisterDate.UserName,
                City = userRegisterDate.City,
                PostacCode = userRegisterDate.PostCode,
                Address = userRegisterDate.Address,
                Country = userRegisterDate.Country,
                PhoneNumber = userRegisterDate.PhoneNumber,
                //Password = userRegisterDate.Password,
                //ConfirmPassword = userRegisterDate.ConfirmPassword,

            };
         
            await _userManager.CreateAsync(newUser, userRegisterDate.Password);


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}

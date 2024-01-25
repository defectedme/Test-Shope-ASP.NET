using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopeCategoryController : Controller
    {

        private readonly TestShopDbContext _context;

        public ShopeCategoryController(TestShopDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            var categorytype = _context.ShopeCategories.ToList();
            ViewData["Categories"] = categorytype;
            var categories = await _context.ShopeCategories.ToListAsync();
            return View(categories);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ShopeCategory shopeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopeCategory);
        }

    }


}

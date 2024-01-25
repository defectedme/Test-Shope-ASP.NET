using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Context.Services;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopToolsController : Controller
    {
        private readonly IShopeToolsServices _services;

        public ShopToolsController(IShopeToolsServices services)
        {
            _services = services;

        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var showAll = await _services.GetAll();
            return View(showAll);
        }

        // GET: ShopeProduct/Details
        public async Task<IActionResult> Details(int id)
        {

            var vviwe = await _services.GetProductById(id);
            return View(vviwe);
        }



        [Authorize]
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Description", "Color", "Price", "UrlPicture")] ShopProduct shopProduct)
        {
            if (ModelState.IsValid)
            {
                await _services.AddAsync(shopProduct);
            }

            return RedirectToAction("Index", "ShopTools");

        }



        [Authorize]

        public async Task<IActionResult> Edit(int id)
        {
            if (_services.GetProductById == null)
            {
                return NotFound();
            }

            var showAll = await _services.GetProductById(id);
            return View(showAll);
        }

    
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ShopProduct shopProduct)
        {

            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "ShopeCategory");
            }


            if (ModelState.IsValid)
            {
                var catId = shopProduct.Category_Id = 3;
                await _services.Update(catId, shopProduct);
            }
            return RedirectToAction("Index", "ShopTools");


        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_services.GetProductById == null)
            {
                return NotFound();
            }

            var delete = await _services.GetProductById(id);
            return View(delete);

        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_services.GetProductById == null)
            {
                return Problem("Entity set 'OnlineBikeShopDbContext.Categories'  is null.");
            }
            if (ModelState.IsValid)
            {
                await _services.Delete(id);
            }


            return RedirectToAction(nameof(Index));

        }

    }
}

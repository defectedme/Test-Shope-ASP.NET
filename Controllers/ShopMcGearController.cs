using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_Shope_ASP.NET.Context.Services;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopMcGearController : Controller
    {
        private readonly IShopeServices _services;

        public ShopMcGearController(IShopeServices services)
        {
            _services = services;

        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var data = await _services.GetAll();
            return View(data);
        }

        // GET: ShopeProduct/Details
        public async Task<IActionResult> Details(int id)
        {
            var mcGearDetail = await _services.GetProductById(id);
            if (mcGearDetail == null)
                return View("Empty");
            return View(mcGearDetail);
        }

        [Authorize(Roles = "Admin")]
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Description", "Color", "Price", "UrlPicture")] ShopProduct shopProduct)
        {
            if (ModelState.IsValid)
            {
                await _services.AddAsync(shopProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(shopProduct);

        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {

            var productEdit = await _services.GetProductById(id);
            return View(productEdit);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShopProduct shopProduct)
        {


            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "ShopeCategory");
            }

            var catId = shopProduct.Category_Id = 2;
            var edit = await _services.Update(catId, shopProduct);
            return RedirectToAction("Index", "ShopMcGear");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var deletView = await _services.GetProductById(id);
            return View(deletView);


        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _services.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

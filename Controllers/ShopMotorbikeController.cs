using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_Shope_ASP.NET.Context.Services;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopMotorbikeController : Controller
    {
        private readonly IShopMotorbikeServices _services;

        public ShopMotorbikeController(IShopMotorbikeServices services)
        {
            _services = services;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {


            var indexShow = await _services.GetAll();
            if (indexShow == null)
            {
                return View("Index");
            }
            return View(indexShow);

        }

        // GET: ShopeProduct/Details
        public async Task<IActionResult> Details(int id)
        {
            if (_services.GetProductById == null)
            {
                return NotFound();
            }



            var detailGet = await _services.GetProductById(id);
            if (detailGet == null)
            {
                return NotFound();
            }
            return View(detailGet);
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
            if (!ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }
            //return View(shopProduct);
            await _services.AddAsync(shopProduct);
            return RedirectToAction("Index", "ShopMotorbike");


        }



        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)

            {
                return View("Index");
            }



            var edit = await _services.GetProductById(id);
            return View(edit);
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

            var catId = shopProduct.Category_Id = 1;
            await _services.Update(catId, shopProduct);
            return RedirectToAction("Index", "ShopMotorbike");
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
            return RedirectToAction("Index", "ShopMotorbike");
        }




    }
}

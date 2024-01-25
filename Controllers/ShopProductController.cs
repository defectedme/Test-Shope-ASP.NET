using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopProductController : Controller
    {
        private readonly TestShopDbContext _context;

        public ShopProductController(TestShopDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.ShopProducts.ToListAsync();
            return View(allProducts);
        }

        // GET: ShopeProduct/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShopProducts == null)
            {
                return NotFound();
            }

            var shopeProducts = await _context.ShopProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopeProducts == null)
            {
                return NotFound();
            }

            return View(shopeProducts);
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
        public async Task<IActionResult> Create([Bind("Name", "Description", "Color", "Category_Id")] ShopProduct shopProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopProduct);
        }



        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShopProducts == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProducts.FindAsync(id);
            if (shopProduct == null)
            {
                return NotFound();
            }

            return View(shopProduct);
        }

        //// POST: ShopeProdct/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Color,Category_Id")] ShopProduct shopProduct)
        {
            if (id != shopProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(shopProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "ShopeCategory");
            }

            return View(shopProduct);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShopProducts == null)
            {
                return NotFound();
            }

            var products = await _context.ShopProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShopProducts == null)
            {
                return Problem("Entity set 'OnlineBikeShopDbContext.Categories'  is null.");
            }
            var categories = await _context.ShopProducts.FindAsync(id);
            if (categories != null)
            {
                _context.ShopProducts.Remove(categories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Motorbike()
        {
            var Data = await _context.ShopProducts.Where(m => m.Category_Id == 1).ToListAsync();
            return View(Data);
        }

        public async Task<ActionResult> Tools()
        {
            var Data = await _context.ShopProducts.Where(m => m.Category_Id == 2).ToListAsync();
            return View(Data);
        }
        public async Task<ActionResult> McGear()
        {
            var Data = await _context.ShopProducts.Where(m => m.Category_Id == 3).ToListAsync();
            return View(Data);
        }

    }
}

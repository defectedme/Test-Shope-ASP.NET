using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Context;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopeAllProductsController : Controller
    {
        private readonly TestShopDbContext _cotext;
        public ShopeAllProductsController(TestShopDbContext cotext)
        {
            _cotext = cotext;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: ShopeProduct/Details
        public async Task<IActionResult> Details(int id)
        {
            if (_cotext.ShopProducts == null)
            {
                return NotFound();
            }

            var products = await _cotext.ShopProducts

                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _cotext.ShopProducts.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allProducts.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);

            }


            else
            {
                return View("NotFound");

            }

        }
    }
}



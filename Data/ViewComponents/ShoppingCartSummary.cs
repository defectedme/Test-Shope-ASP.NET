using Microsoft.AspNetCore.Mvc;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models.BusinessLogic;

namespace Test_Shope_ASP.NET.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly TestShopDbContext _context;
        private string IdCartSesion;

        public ShoppingCartSummary(TestShopDbContext context)
        {
            _context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);
            var items = await cartLogic.GetCartItems();
            return View(items.Count);
        }
    }
}

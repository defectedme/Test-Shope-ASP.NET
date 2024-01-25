using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Context;

namespace Test_Shope_ASP.NET.Models.BusinessLogic
{
    public class CartLogic 
    {

        private readonly TestShopDbContext _context;

        public string IdCartSesion { get; set; }
        public List<ShopCartItemModel> ShoppingCartItems { get; set; }


        public CartLogic(TestShopDbContext context, HttpContext httpContext)
        {
            _context = context;
            this.IdCartSesion = GetIdSesjiBasket(httpContext);
        }
        private string GetIdSesjiBasket (HttpContext httpContext) 
        {
            //Jeżeli w Sesji IdSesjiKoszyka jest null-em
            if (httpContext.Session.GetString("IdCartSesion") == null)
            {
                //Jeżeli context.User.Identity.Name nie jest puste i nie posiada białych zanków
                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    httpContext.Session.SetString("IdCartSesion", httpContext.User.Identity.Name);
                }
                else
                {
                    // W przeciwnym wypadku wygeneruj przy pomocy random Guid IdSesjiKoszyka
                    Guid tempIdCartSesion = Guid.NewGuid();
                    // Wyślij wygenerowane IdSesjiKoszyka jako cookie
                    httpContext.Session.SetString("IdCartSesion", tempIdCartSesion.ToString());
                }
            }
            return httpContext.Session.GetString("IdCartSesion").ToString();
        }





        public void AddToCart(ShopProduct shopProduct)
        {
            //Najpierw sprawdzamy czy dany towar już istnieje w koszyku danego klienta
            var shopCartItemModel =
               (
                   from element in _context.ShopCartItemModel
                   where element.IdCartSesion == this.IdCartSesion && element.IdProduct == shopProduct.Id
                   select element
               ).FirstOrDefault();


            // jeżeli brak tego towaru w koszyku
            if (shopCartItemModel == null)
            {
                // Wtedy tworzymy nowy element w koszyku
                shopCartItemModel = new ShopCartItemModel()
                {
                    IdCartSesion = this.IdCartSesion,
                    IdProduct = shopProduct.Id,
                    ShopProduct = _context.ShopProducts.Find(shopProduct.Id),
                    Quantity = 1,
                    CreationDate = DateTime.Now
                };
                //i dodajemy do kolekcji lokalne
                _context.ShopCartItemModel.Add(shopCartItemModel);
            }
            else
            {
                // Jeżeli dany towar istnieje już w koszyku to liczbe sztuk zwiekszamy o 1
                shopCartItemModel.Quantity++;
            }
            // Zapisujemy zmiany do bazy
            _context.SaveChanges();
        }
        public async Task<List<ShopCartItemModel>> GetCartItems()
        {
            return await
               _context.ShopCartItemModel.Where(e => e.IdCartSesion == this.IdCartSesion).Include(e => e.ShopProduct).ToListAsync();
        }

        public List<ShopCartItemModel> GetCartItemss()
        {

            return
             _context.ShopCartItemModel.Where(e => e.IdCartSesion == this.IdCartSesion).Include(e => e.ShopProduct).ToList();
        }




        public async Task<double> GetTotal()
        {
            var item =
                (
                from element in _context.ShopCartItemModel
                where element.IdCartSesion == this.IdCartSesion
                select element.Quantity * element.ShopProduct.Price
                );
            return await item.SumAsync();
        }




        public void DeleteFromCartta(ShopProduct shopProduct)
        {
            var shoppingCartItem = _context.ShopCartItemModel.FirstOrDefault(n => n.ShopProduct.Id == shopProduct.Id && n.IdCart == n.IdCart);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                }
                else
                {
                    _context.ShopCartItemModel.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }


        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShopCartItemModel.Where(n => n.IdCart == n.IdCart && n.IdCartSesion == n.IdCartSesion).FirstAsync();
            _context.ShopCartItemModel.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}

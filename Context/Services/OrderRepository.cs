using System.Xml.Linq;
using Test_Shope_ASP.NET.Models;
using Test_Shope_ASP.NET.Models.BusinessLogic;

namespace Test_Shope_ASP.NET.Context.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestShopDbContext _context;
        private readonly CartLogic _cartLogic;
        public OrderRepository(TestShopDbContext context, CartLogic cartLogic)
        {

            _cartLogic = cartLogic;
            _context = context;
        }

        public async Task CreateOrder(ShopOrder shopOrder)
        {
            shopOrder.DateOfOrder = DateTime.Now;
            _context.ShopOrders.Add(shopOrder);
            var shopCartItemModel = _cartLogic.ShoppingCartItems;

            foreach (var element in shopCartItemModel)
            {
                var orderItem = new ShopOrderItem()
                {
                    ProductId = element.IdProduct,
                    OrderId = shopOrder.IdOrder,
                    Price = element.ShopProduct.Price,
                    Quantity = element.Quantity,

                };
                await _context.ShopOrderItems.AddAsync(orderItem);

            }
            await _context.SaveChangesAsync();
        }


    }
}

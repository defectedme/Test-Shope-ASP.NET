using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Test_Shope_ASP.NET.Clients;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;
using Test_Shope_ASP.NET.Models.BusinessLogic;
using Test_Shope_ASP.NET.Models.Shop;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopOrderController : Controller
    {


        List<ShopCartItemModel> student = new List<ShopCartItemModel>();

        private readonly PaypalClient _paypalClient;


        private readonly TestShopDbContext _context;

        public string IdCartSesion { get; set; }


        public ShopOrderController(PaypalClient paypalClient, TestShopDbContext context)
        {

            _context = context;
            this._paypalClient = paypalClient;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ShopOrderItems.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Zamowienie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,Price,Total, ProductId, OrderId, IdOrderItem")] ShopOrderItem zamowienie)
        {


            if (ModelState.IsValid)
            {
                _context.Add(zamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zamowienie);

        }

        public void CreateOrder(ShopOrder shopOrder)
        {

            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);

            shopOrder.DateOfOrder = DateTime.Now;
            _context.ShopOrders.Add(shopOrder);

            var shoppingCartItems = cartLogic.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new ShopOrderItem()
                {
                    //Quantity = item.Quantity,
                    //Price = item.Quantity * 100,
                    OrderId = shopOrder.IdOrder,
                    ProductId = item.IdProduct,
                    //Price = item.ShopProduct.Price,
                };
                _context.ShopOrderItems.Add(orderDetail);
            }
            _context.SaveChanges();

        }



        public IActionResult Dane()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dane([Bind("Name,LastName, PhoneNumber, City, Street, PostCode, Country, Email")] ShopOrder shopOrder)
        {


            if (ModelState.IsValid)
            {
                shopOrder.DateOfOrder = DateTime.Now;
                await _context.AddAsync(shopOrder);

                CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);
                var cartItems = await cartLogic.GetCartItems();

                foreach (var element in cartItems)
                {
                    var pozycjaZamowienia = new ShopOrderItem
                    {
                        
                        ProductId = element.IdProduct,
                        OrderId = shopOrder.IdOrder,
                        Price = element.ShopProduct.Price,
                        Quantity = element.Quantity

                    };
                    await _context.ShopOrderItems.AddAsync(pozycjaZamowienia);
                }

                shopOrder.Total = await cartLogic.GetTotal();
                await _context.SaveChangesAsync();
                await cartLogic.ClearShoppingCartAsync();

                return RedirectToAction("OrderDetailInfo", new { id = shopOrder.IdOrder });
                //return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task<ActionResult> OrderDetailInfo(int id)
        {
            var shopOrder = await _context.ShopOrders.FirstOrDefaultAsync(z => z.IdOrder == id);
            if (shopOrder == null)
            {
                return View("Error");
            }
            return View(shopOrder);
        }


        //PayPal
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        public IActionResult Success()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken, ShopOrder shopOrder)
        {


            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);

            var shopOrderpaypal = await cartLogic.GetTotal();


            var price = shopOrderpaypal.ToString();
            var currency = "USD";

            // "reference" is the transaction key
            var reference = "INV001";

            var response = await _paypalClient.CreateOrder(price, currency, reference);

            return Ok(response);

        }










    }
}

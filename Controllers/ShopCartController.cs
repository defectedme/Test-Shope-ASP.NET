using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;
using System.Collections.Generic;
using System.Xml.Schema;
using Test_Shope_ASP.NET.Clients;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Models;
using Test_Shope_ASP.NET.Models.BusinessLogic;
using Test_Shope_ASP.NET.Models.Shop;

namespace Test_Shope_ASP.NET.Controllers
{



    public class ShopCartController : Controller
    {

        private readonly PaypalClient _paypalClient;

        private readonly TestShopDbContext _context;

        public ShopCartController(PaypalClient paypalClient, TestShopDbContext context)
        {
            _context = context;
            this._paypalClient = paypalClient;

        }
        public async Task<ActionResult> Index()
        {
            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);
            var cartElements = new CartElements
            {
                ShopCartItemModel = await cartLogic.GetCartItems(),
                Total = (decimal)await cartLogic.GetTotal()
            };
            return View(cartElements);
        }

        [Authorize]
        public async Task<ActionResult> AddToCart(int id)
        {
            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);
            cartLogic.AddToCart(await _context.ShopProducts.FindAsync(id));
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public async Task<ActionResult> DeleteFromCart(int id)
        {
            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);

            var item = await cartLogic.GetCartItems();

            if (item != null)
            {
                cartLogic.DeleteFromCartta(await _context.ShopProducts.FindAsync(id));
            }
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> ClearBasket()
        {
            CartLogic cartLogic = new CartLogic(this._context, this.HttpContext);
            var item = await cartLogic.GetCartItems();

            if (item != null)
            {
                await cartLogic.ClearShoppingCartAsync();
            }

            return RedirectToAction(nameof(Index));

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


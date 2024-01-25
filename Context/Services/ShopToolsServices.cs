using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public class ShopToolsServices : IShopeToolsServices
    {
        private readonly TestShopDbContext _context;
        public ShopToolsServices(TestShopDbContext context)
        {

            _context = context;

        }


        public async Task AddAsync(ShopProduct shopProduct)
        {

            var categoryId = shopProduct.Category_Id = 3;
            _context.Add(shopProduct);
            await _context.SaveChangesAsync(shopProduct.Category_Id == categoryId);
        }

        public async Task Delete(int id)
        {

            var delete = await _context.ShopProducts.FindAsync(id);
            _context.ShopProducts.Remove(delete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShopProduct>> GetAll()
        {
            var Data = await _context.ShopProducts.Where(m => m.Category_Id == 3).ToListAsync();
            return Data;
        }

        public async Task<IEnumerable<ShopProduct>> GetProductById(int id)
        {


            var shopeProducts = await _context.ShopProducts.Where(m => m.Id == id).ToListAsync();
            return shopeProducts;
        }



        public async Task<ShopProduct> Update(int id, ShopProduct newShopeProduct)
        {
            _context.Update(newShopeProduct);
            await _context.SaveChangesAsync();
            return newShopeProduct;
        }


    }
}

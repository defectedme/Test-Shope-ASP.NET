using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public class ShopMcGearServices : IShopeServices
    {
        private readonly TestShopDbContext _context;
        public ShopMcGearServices(TestShopDbContext context)
        {

            _context = context;

        }
        public async Task AddAsync(ShopProduct shopProduct)
        {

            var id = shopProduct.Category_Id = 2;
            await _context.ShopProducts.AddAsync(shopProduct);
            await _context.SaveChangesAsync(shopProduct.Category_Id == id);
        }

        public async Task Delete(int id)
        {
            var categories = await _context.ShopProducts.FindAsync(id);
            _context.ShopProducts.Remove(categories);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShopProduct>> GetAll()
        {
            var Data = await _context.ShopProducts.Where(m => m.Category_Id == 2).ToListAsync();
            return Data;
        }

        public async Task<IEnumerable<ShopProduct>> GetProductById(int id)
        {
            var result = await _context.ShopProducts.Where(m => m.Id == id).ToListAsync();
            return result;
        }

  

        public async Task<ShopProduct> Update(int id, ShopProduct newShopeProduct)
        {
            var categoryId = newShopeProduct.Category_Id = 2;
            _context.Update(newShopeProduct);
            await _context.SaveChangesAsync(newShopeProduct.Category_Id == categoryId);
            return newShopeProduct;
        }

    }
}

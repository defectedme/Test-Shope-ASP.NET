using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public class ShopMotorbikeServices : IShopMotorbikeServices
    {
        private readonly TestShopDbContext _contex;
        public ShopMotorbikeServices(TestShopDbContext contex)
        {

            _contex = contex;

        }

        public async Task AddAsync(ShopProduct shopProduct)
        {

            var catID = shopProduct.Category_Id = 1;
            await _contex.AddAsync(shopProduct);
            await _contex.SaveChangesAsync(shopProduct.Category_Id == catID);
        }

        public async Task Delete(int id)
        {
            var categories = await _contex.ShopProducts.FindAsync(id);
            _contex.ShopProducts.Remove(categories);
            await _contex.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShopProduct>> GetAll()
        {
            var Data = await _contex.ShopProducts.Where(m => m.Category_Id == 1).ToListAsync();
            return Data;
        }

        public async Task<IEnumerable<ShopProduct>> GetProductById(int id)
        {
            var Data = await _contex.ShopProducts.Where(m => m.Id == id).ToListAsync();
            return Data;
        }    
        
        public async Task<ShopProduct> Update(int id, ShopProduct newShopeProduct)
        {
            var categoryID = newShopeProduct.Category_Id = 1;
            _contex.Update(newShopeProduct);
            await _contex.SaveChangesAsync(newShopeProduct.Category_Id == 1);
            return null;

        }
    }
}

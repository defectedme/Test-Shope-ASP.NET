using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public interface IShopeServices
    {

        Task<IEnumerable<ShopProduct>> GetAll();

        Task<IEnumerable<ShopProduct>> GetProductById(int id);

        Task AddAsync(ShopProduct shopProduct);

        Task<ShopProduct> Update(int id, ShopProduct newShopeProduct);

        Task Delete(int id);
    }
}

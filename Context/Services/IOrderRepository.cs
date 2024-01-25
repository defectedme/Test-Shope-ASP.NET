using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public interface IOrderRepository
    {
        Task CreateOrder(ShopOrder shopOrder);
    }
}

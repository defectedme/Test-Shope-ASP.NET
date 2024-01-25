using Microsoft.AspNetCore.Identity;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context.Services
{
    public interface IShopUserDetailsServices
    {


        Task<ApplicationUser> Update(ApplicationUser application);




    }
}

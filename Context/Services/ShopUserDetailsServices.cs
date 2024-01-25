using Microsoft.AspNetCore.Identity;
using Test_Shope_ASP.NET.Models;


namespace Test_Shope_ASP.NET.Context.Services
{
    public class ShopUserDetailsServices : IShopUserDetailsServices
    {


        private readonly TestShopDbContext _contex;
        private readonly UserManager<ApplicationUser> _userManager;
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContext.User);



        public ShopUserDetailsServices(TestShopDbContext context, UserManager<ApplicationUser> userManager)
        {
            _contex = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                user.City = applicationUser.City;
                user.FullName = applicationUser.FullName;
                user.Address = applicationUser.Address;
                user.Email = applicationUser.Email;
                user.Country = applicationUser.Country;
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.PostacCode = applicationUser.PostacCode;

                await _contex.SaveChangesAsync();

            }
            catch
            {
                Console.WriteLine("Error");
                throw;
            }


            return null;
        }
    }
}

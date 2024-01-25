using Microsoft.AspNetCore.Identity;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Data.Static;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TestShopDbContext>();

                context.Database.EnsureCreated();


                //categories
                if (!context.ShopeCategories.Any())
                {
                    context.ShopeCategories.AddRange(new List<ShopeCategory>()
                    {
                        new ShopeCategory()
                        {
                            Name = "Motorbike",
                            ImagePath = "https://www.tarform.com/wp-content/uploads/2022/11/TarformScramber1.jpg",


                        },
                        new ShopeCategory()
                        {
                            Name = "Gears",
                          ImagePath = "https://kickstart.bikeexif.com/wp-content/uploads/2017/08/retro-motorcycle-gear-review-1.jpg",


                        },
                        new ShopeCategory()
                        {
                            Name = "Tools",
                            ImagePath = "https://d2hucwwplm5rxi.cloudfront.net/wp-content/uploads/2022/09/14102948/motorcycle-tools-must-haves-_-Body-4-7-9-22-1024x640.jpg",



                        }


                    });
                    context.SaveChanges();
                }

                //Products
                if (!context.ShopProducts.Any())
                {
                    context.ShopProducts.AddRange(new List<ShopProduct>()
                    {
                    new ShopProduct()
                    {
                        Name = "Yamaha",
                        Description ="1000RR",
                        Color = "Red",
                        UrlPicture = "https://www.tarform.com/wp-content/uploads/2022/11/TarformScramber1.jpg",

                        Category_Id = 1,
                    },
                    new ShopProduct()
                    {
                        Name = "Wrench",
                        Description ="10mm",
                        Color = "Metal",

                        UrlPicture = "https://productimages.biltema.com/v1/image/app/imagebyfilename/71-210_xl_1.jpg",
                        Category_Id = 3,
                    },
                    new ShopProduct()
                    {
                        Name = "Halmet",
                        Description ="S Size",
                        Color = "Black",

                        UrlPicture = "https://manofmany.com/wp-content/uploads/2017/08/ScorpionExo-Covert-Unisex-Adult-Half-Size-Style-Matte-Black-Helmet-.jpg",
                        Category_Id = 2,

                    }
                    });
                    context.SaveChanges();
                }





            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";
                string adminUserPhone = "22554433";


                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        NormalizedUserName = "Admin",
                        Address = "Wolna",
                        City = "Warszawa",
                        PostacCode = "22-222",
                        UserName = "admin",
                        FullName = "Marek Wozniak",
                        Country = "Germany",
                        //Password = "admin!",
                        //ConfirmPassword = "admin!",



                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = adminUserPhone,
                        PhoneNumberConfirmed = true
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "AdminAdmin!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

               
                }


             
            }
        }


    }
}

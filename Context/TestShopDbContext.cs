using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Context
{
    public class TestShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public TestShopDbContext(DbContextOptions<TestShopDbContext> options) : base(options)
        {

        }

        public TestShopDbContext()
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<ShopProduct>().Property(e => e.Category_Id);
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<ShopeCategory> ShopeCategories { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<ShopCartItemModel> ShopCartItemModel { get; set; }
        public DbSet<ShopOrder> ShopOrders { get; set; }
        public DbSet<ShopOrderItem> ShopOrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ShopProduct>().Property(e => e.Category_Id);
            base.OnModelCreating(builder);


            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }




    }
}

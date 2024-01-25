using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Test_Shope_ASP.NET.Clients;
using Test_Shope_ASP.NET.Context;
using Test_Shope_ASP.NET.Context.Services;
using Test_Shope_ASP.NET.Data;
using Test_Shope_ASP.NET.Hubs;
using Test_Shope_ASP.NET.Models;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Builder.Extensions;
using System.Configuration;
using Test_Shope_ASP.NET.Models.MailConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<SecurityStampValidatorOptions>(o =>
                   o.ValidationInterval = TimeSpan.FromMinutes(1));


//signalIR
builder.Services.AddSignalR();

//Send email
//builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));



builder.Services.AddScoped<TestShopDbContext>();

//Adding services (Interface) 
builder.Services.AddScoped<IShopeServices, ShopMcGearServices>();
builder.Services.AddScoped<IShopeToolsServices, ShopToolsServices>();
builder.Services.AddScoped<IShopMotorbikeServices, ShopMotorbikeServices>();
builder.Services.AddScoped<IShopUserDetailsServices, ShopUserDetailsServices>();



//Emails 


//builder.Services.AddTransient<IMailService, MailService>();

//builder.Services.AddTransient<IEmailSender, EmailSender>();

//builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);




builder.Services.AddControllers();



//Services to CartItem
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(1);
    options.Cookie.HttpOnly = false;
    // Make the session cookie essential
    options.Cookie.IsEssential = false;
});





//Services to CartItem it will newver return same cart for 2 diffent accounts
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// paypal client configuration
builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);

builder.Services.AddMemoryCache();







builder.Services.AddDbContext<TestShopDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;

    })
    .AddEntityFrameworkStores<TestShopDbContext>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseCookiePolicy();

app.UseSession();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");



//Adding users and some products to db
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();




//Chay hub settings

//app.MapHub<ChatHub>("/chatHub");


app.MapHub<ChatHub>("/chatHub", options =>
{
    options.Transports = HttpTransportType.LongPolling;
});



app.Run();

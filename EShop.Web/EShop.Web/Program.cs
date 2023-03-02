using Eshop.Repository.Data;
using EShop.Domain.Models;
using EShop.Domain.Settings;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Implementation;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

public class Program
{
    private static EmailSettings emailSettings;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        //  builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        /*
         * "EmailSettings": {
      "SmtpServer": "smtp.gmail.com",
      "SmtpUserName": "mail",
      "SmtpPassword": "pass",
      "SmtpServerPort": 587,
      "EnableSsl": true,
      "EmailDisplayName": "EShop Application",
      "SendersName": "EShop Application"
         */
        builder.Configuration.GetSection("EmailSettings").Bind(new EmailSettings
        {
            SmtpUserName = "ishwtestmail@gmail.com",
            SmtpServer = "smtp.gmail.com",
            SmtpPassword = "Test123!",
            SmtpServerPort = 587,
        });

        builder.Services.AddDefaultIdentity<Client>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

        builder.Services.AddScoped(typeof(ICarPartRepository), typeof(CarPartRepository));

        builder.Services.AddScoped(typeof(IShoppingCartRepository), typeof(ShoppingCartRepository));

        builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

        builder.Services.AddScoped(typeof(IShoppingCartContainsCarPartRepository), typeof(ShoppingCartContainsCarPartRepository));

        builder.Services.AddScoped(typeof(IOrderContainsCarPartRepository), typeof(OrderContainsCarPartRepository));



        builder.Services.AddTransient<ICarPartService, CarPartService>();

        builder.Services.AddTransient<ICarPartStageService, CarPartStageService>();


        builder.Services.AddTransient<ICarPartBrandService, CarPartBrandService>();

        builder.Services.AddTransient<ICarPartTypeService, CarPartTypeService>();



        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IUserService, UserService>();

        builder.Services.AddTransient<IShoppingCartContainsCarPartService, ShoppingCartContainsCarPartService>();

        builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

        var app = builder.Build();

        StripeConfiguration.ApiKey = (app.Configuration.GetSection("Stripe")["SecretKey"]);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
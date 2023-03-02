using EShop.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public ApplicationDbContext()
        {

        }
        public DbSet<CarPart> CarParts { get; set; }
        public DbSet<CarPartBrand> CarPartBrands { get; set; }

        public DbSet<EmailMessage> EmailMessages { get; set; }
      

        public DbSet<CarPartInStockAtDealership> CarPartInStockAtDealerships { get; set; }

        public DbSet<CarPartStage> CarPartStages { get; set; }

        public DbSet<CarPartType> CarPartTypes { get; set; }

        public DbSet<Dealership> Dealerships { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> ShopppingCarts { get; set; }

        public DbSet<ShoppingCartContainsCarPart> ShoppingCartContainsCarParts { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<OrderContainsCarPart> OrderContainsCarParts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           // builder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
          //  builder.Entity<IdentityUserRole>().HasKey(u => u.UserId);

            //Address

         /*   
            builder.Entity<Address>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
         */



            //CarPart

            builder.Entity<CarPart>()
                .Property(cp => cp.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CarPart>()
                  .HasOne(cp => cp.Type)
                  .WithMany(cp => cp.CarParts)
                  .HasForeignKey(cp => cp.CarPartTypeId);

            builder.Entity<CarPart>()
                .HasOne(cp => cp.Stage)
                .WithMany(cp => cp.CarParts)
                .HasForeignKey(cp => cp.CarPartStageId);

            builder.Entity<CarPart>()
              .HasOne(cp => cp.Brand)
              .WithMany(cp => cp.CarParts)
              .HasForeignKey(cp => cp.CarPartBrandId);

            //CarPartStage

            builder.Entity<CarPartStage>()
              .Property(cpb => cpb.Id)
              .ValueGeneratedOnAdd();



            //CarPartBrand


            builder.Entity<CarPartBrand>()
                .Property(cpb => cpb.Id)
                .ValueGeneratedOnAdd();

            //CarPartType


            builder.Entity<CarPartType>()
              .Property(cpb => cpb.Id)
              .ValueGeneratedOnAdd();

            //Dealership

            builder.Entity<Dealership>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            /*
            builder.Entity<Dealership>()
                .HasOne(d => d.Address)
                .WithOne(d => d.Dealership)
                .HasForeignKey<Dealership>(d => d.AddressId);

            */

            //Client
            
            /*
            builder.Entity<Client>()
                .HasOne(c => c.Address)
                .WithOne(c => c.Client)
                .HasForeignKey<Client>(c => c.AddressId);
            */
           

            //ShoppingCart

            builder.Entity<ShoppingCart>()
              .Property(sc => sc.Id)
              .ValueGeneratedOnAdd();

            

            builder.Entity<ShoppingCart>()
               .HasOne(c => c.Client)
               .WithOne(c => c.ShoppingCart)
               .HasForeignKey<ShoppingCart>(c => c.ClientId);


            //Order

            builder.Entity<Order>()
              .Property(o => o.Id)
              .ValueGeneratedOnAdd();

            builder.Entity<Order>()
                   .HasOne(o => o.Client)
                   .WithMany(o => o.Orders)
                   .HasForeignKey(o => o.ClientId);


            //ShoppingCartContainsCarPart

            builder.Entity<ShoppingCartContainsCarPart>()
              .HasKey(scccp => new { scccp.ShoppingCartId, scccp.CarPartId });


            builder.Entity<ShoppingCartContainsCarPart>()
                .HasOne(scccp => scccp.CarPart)
                .WithMany(scccp => scccp.ShoppingCartContainsCarParts)
                .HasForeignKey(scccp => scccp.CarPartId);

            builder.Entity<ShoppingCartContainsCarPart>()
               .HasOne(scccp => scccp.ShoppingCart)
               .WithMany(scccp => scccp.ShoppingCartContainsCarParts)
               .HasForeignKey(scccp => scccp.ShoppingCartId);

            //CarPartInStockAtDealership

            builder.Entity<CarPartInStockAtDealership>()
                .HasKey(cpisad => new { cpisad.CarPartId, cpisad.DealershipId });

            builder.Entity<CarPartInStockAtDealership>()
                .HasOne(cpisad => cpisad.Dealership)
                .WithMany(cpisad => cpisad.CarPartInStockAtDealerships)
                .HasForeignKey(cpisad => cpisad.DealershipId);

            builder.Entity<CarPartInStockAtDealership>()
                .HasOne(cpisad => cpisad.CarPart)
                .WithMany(cpisad => cpisad.CarPartInStockAtDealerships)
                .HasForeignKey(cpisad => cpisad.CarPartId);

            //OrderContainsCarPart

            builder.Entity<OrderContainsCarPart>()
                .HasKey(occp => new { occp.OrderId, occp.CarPartId });

            builder.Entity<OrderContainsCarPart>()
                .HasOne(occp => occp.Order)
                .WithMany(occp => occp.OrderContainsCarParts)
                .HasForeignKey(occp => occp.OrderId);

            builder.Entity<OrderContainsCarPart>()
                .HasOne(occp => occp.CarPart)
                .WithMany(occp => occp.OrderContainsCarParts)
                .HasForeignKey(occp => occp.CarPartId);



        }

       
    }
}
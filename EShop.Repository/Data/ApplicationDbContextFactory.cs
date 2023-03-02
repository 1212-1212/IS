using Eshop.Repository.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-EShop.Web-083dd22e-e973-429c-91ea-2177f5bc34e6;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

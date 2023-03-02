using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface ICarPartBrandService
    {
        List<CarPartBrand> GetAll();
        CarPartBrand Get(Guid? id);
        void Create(CarPartBrand car);
        void Update(CarPartBrand t);
        void Delete(Guid id);

        CarPartBrand getByCountryAndManufacturer(string manufacturerName, string countryName);
    }
}

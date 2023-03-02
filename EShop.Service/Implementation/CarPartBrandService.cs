using EShop.Domain.Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class CarPartBrandService : ICarPartBrandService
    {
        private readonly IRepository<CarPartBrand> _repository;


         public CarPartBrandService(IRepository<CarPartBrand> repository)
        {
            _repository = repository;
        }

        public void Create(CarPartBrand carPartBrand)
        {
           _repository.Insert(carPartBrand);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(Get(id));
        }

        public CarPartBrand Get(Guid? id)
        {
          return  _repository.Get(id);
        }

        public List<CarPartBrand> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public CarPartBrand getByCountryAndManufacturer(string manufacturerName, string countryName)
        {
            return _repository.GetAll()
                .Where(item => item.Country == countryName && item.BrandName == manufacturerName)
                .FirstOrDefault();
        }

        public void Update(CarPartBrand carPartBrand)
        {
            _repository.Update(carPartBrand);
        }
    }
}

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
    public class CarPartTypeService : ICarPartTypeService

    {

        private readonly IRepository<CarPartType> _repository;

        public CarPartTypeService(IRepository<CarPartType> repository)
        {
            _repository = repository;
        }

        public void Create(CarPartType carPartType)
        {
            _repository.Insert(carPartType);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(Get(id));
        }

        public CarPartType Get(Guid? id)
        {
            return _repository.Get(id);
        }

        public List<CarPartType> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public CarPartType GetByType(string type)
        {
            return _repository.GetAll()
                .Where(item => item.Type == type)
                .FirstOrDefault();
        }

        public void Update(CarPartType carPartType)
        {
            _repository.Update(carPartType);
        }
    }
}

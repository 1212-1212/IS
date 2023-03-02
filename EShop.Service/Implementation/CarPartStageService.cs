using EShop.Domain.Models;
using EShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public class CarPartStageService : ICarPartStageService
    {
        private readonly IRepository<CarPartStage> _repository;

        public CarPartStageService(IRepository<CarPartStage> repository)
        {
            _repository = repository;
        }

        public void Create(CarPartStage carPartStage)
        {
            _repository.Insert(carPartStage);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(Get(id));
        }

        public CarPartStage Get(Guid? id)
        {
            return _repository.Get(id);
        }

        public List<CarPartStage> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public CarPartStage GetByStage(string stage)
        {
            return _repository.GetAll()
                .Where(item => item.Stage == stage)
                .FirstOrDefault();
        }

        public void Update(CarPartStage carPartStage)
        {
            _repository.Update(carPartStage);
        }
    }
}

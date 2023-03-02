using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface ICarPartTypeService
    {
        List<CarPartType> GetAll();
        CarPartType Get(Guid? id);
        void Create(CarPartType car);
        void Update(CarPartType t);
        void Delete(Guid id);

        CarPartType GetByType(string type);
    }
}

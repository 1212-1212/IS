using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface ICarPartStageService
    {
        List<CarPartStage> GetAll();
        CarPartStage Get(Guid? id);
        void Create(CarPartStage carPartStage);
        void Update(CarPartStage t);
        void Delete(Guid id);

        CarPartStage GetByStage(string stage);

    }
}

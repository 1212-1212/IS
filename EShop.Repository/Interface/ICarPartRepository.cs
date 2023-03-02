using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface ICarPartRepository
    {
        IEnumerable<CarPart> GetAll();
        CarPart Get(Guid? id);
        void Create(CarPart carPart);
        void Update(CarPart carPart);
        void Delete(CarPart carPart);


        
    }
}

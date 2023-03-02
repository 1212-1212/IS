using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface ICarPartService
    {
        IEnumerable<CarPart> GetAll();
        CarPart Get(Guid? id);
        void Create(CarPart car);
        void Update(CarPart t);
        void Delete(Guid id);
        IEnumerable<CarPart> SearchBySearchString(string searchString);

        IEnumerable<CarPart> SearchByBrandName(CarPartBrand brandName);

        IEnumerable<CarPart> SearchByType(CarPartType type);

        IEnumerable<CarPart> SearchByStage(CarPartStage stage);

        ShoppingCart AddToShoppingCart(CarPart carPart, ShoppingCart shoppingCart);
     
    }
}

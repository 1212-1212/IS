using EShop.Domain.Models;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class CarPartService : ICarPartService
    {
        private readonly ICarPartRepository _carPartRepository;
        private readonly IShoppingCartContainsCarPartRepository _shoppingCartContainsRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository; 

        public CarPartService(ICarPartRepository carPartRepository, IShoppingCartContainsCarPartRepository shoppingCartContainsRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _carPartRepository = carPartRepository;
            _shoppingCartContainsRepository = shoppingCartContainsRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void Create(CarPart carPart)
        { 
            _carPartRepository.Create(carPart);
        }
        public void Update(CarPart carPart)
        {
            _carPartRepository.Update(carPart);
        }

        
        public void Delete(Guid id)
        {
            _carPartRepository.Delete(Get(id));
        }

        public CarPart Get(Guid? id)
        {
            return _carPartRepository.Get(id);
        }

       

        public IEnumerable<CarPart> SearchByBrandName(CarPartBrand brandName)
        {

            return _carPartRepository.GetAll()
                 .Where(cp => cp.Brand.Equals(brandName));
        }

        public IEnumerable<CarPart> SearchBySearchString(string searchString)
        {
            return _carPartRepository.GetAll()
                 .Where(cp => cp.Description.Contains(searchString));
        }

        public IEnumerable<CarPart> SearchByStage(CarPartStage stage)
        {
            return _carPartRepository.GetAll()
                 .Where(cp => cp.Stage.Equals(stage));
        }

        public IEnumerable<CarPart> SearchByType(CarPartType type)
        {
            return _carPartRepository.GetAll()
                 .Where(cp => cp.Type.Equals(type));
        }

        public IEnumerable<CarPart> GetAll()
        {
            return _carPartRepository.GetAll();
        }

        public ShoppingCart AddToShoppingCart(CarPart carPart, ShoppingCart shoppingCart)
        {
   
            var quantity = 1;
            ShoppingCartContainsCarPart result;
            var existing = _shoppingCartContainsRepository.Get(carPart.Id, shoppingCart.Id);

            if (existing != null)
            {
                existing.Quantity = existing.Quantity + 1;
                _shoppingCartContainsRepository.Update(existing);
                shoppingCart.ShoppingCartContainsCarParts.Remove(existing);
                result = existing;
            }
            else
            {

                var carPartInShoppingCart = new ShoppingCartContainsCarPart
                {
                    CarPartId = carPart.Id,
                    ShoppingCartId = shoppingCart.Id,
                    CarPart = carPart,
                    ShoppingCart = shoppingCart,
                    Quantity = quantity
                };
                _shoppingCartContainsRepository.Insert(carPartInShoppingCart);
                result = carPartInShoppingCart;
                
            }
            shoppingCart.ShoppingCartContainsCarParts.Add(result);
            _shoppingCartRepository.Update(shoppingCart);

            return shoppingCart;

        }
    }
}

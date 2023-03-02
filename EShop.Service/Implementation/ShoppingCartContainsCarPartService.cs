using EShop.Domain.Models;
using EShop.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartContainsCarPartService : IShoppingCartContainsCarPartService
    {

        private readonly IShoppingCartContainsCarPartRepository _repository;
   

        public ShoppingCartContainsCarPartService(IShoppingCartContainsCarPartRepository repository)
        {
            _repository = repository;
        }

        public void Delete(ShoppingCartContainsCarPart entity)
        {
            _repository.Delete(entity);
            
        }

        public ShoppingCartContainsCarPart Get(Guid? carPartId, Guid? shoppingCartId)
        {
           return _repository.Get(carPartId, shoppingCartId);
        }

        public IEnumerable<ShoppingCartContainsCarPart> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ShoppingCartContainsCarPart> GetAllFromShoppingCart(ShoppingCart shoppingCart)
        {
            return _repository.GetAllFromShoppingCart(shoppingCart);
        }

        public void Insert(ShoppingCartContainsCarPart entity)
        {
             _repository.Insert(entity);
        }

        public void Update(ShoppingCartContainsCarPart entity)
        {
            _repository.Update(entity);
        }

        public List<OrderContainsCarPart> toOrderContainsCarPart(List<ShoppingCartContainsCarPart> list, Order order)
        {
            return list
                .Select(item => new OrderContainsCarPart
                {
                    CarPartId = item.CarPartId,
                    CarPart = item.CarPart,
                    Order = order,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                }).ToList();
        }
    }
}

using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public interface IShoppingCartContainsCarPartService
    {
        IEnumerable<ShoppingCartContainsCarPart> GetAll();
        ShoppingCartContainsCarPart Get(Guid? carPartId, Guid? shoppingCartId);
        void Insert(ShoppingCartContainsCarPart entity);
        void Update(ShoppingCartContainsCarPart entity);
        void Delete(ShoppingCartContainsCarPart entity);
        IEnumerable<ShoppingCartContainsCarPart> GetAllFromShoppingCart(ShoppingCart shoppingCart);

        public List<OrderContainsCarPart> toOrderContainsCarPart(List<ShoppingCartContainsCarPart> list, Order order);
       
    }
}

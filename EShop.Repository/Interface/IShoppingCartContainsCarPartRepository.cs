using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementation
{
    public interface IShoppingCartContainsCarPartRepository
    {
        IEnumerable<ShoppingCartContainsCarPart> GetAll();
        ShoppingCartContainsCarPart Get(Guid? carPartId, Guid? shoppingCartId);
        void Insert(ShoppingCartContainsCarPart entity);
        void Update(ShoppingCartContainsCarPart entity);
        void Delete(ShoppingCartContainsCarPart entity);
        IEnumerable<ShoppingCartContainsCarPart> GetAllFromShoppingCart(ShoppingCart shoppingCart);

    }
}

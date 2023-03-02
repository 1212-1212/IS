using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IShoppingCartRepository
    {
        ShoppingCart Get(Guid shoppingCartId);

        void Order(Client client);
        void Insert(ShoppingCart entity);
        void Update(ShoppingCart entity);
        void Delete(ShoppingCart entity);
    }
}

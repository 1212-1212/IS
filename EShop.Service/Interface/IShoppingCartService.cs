using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart Get(Guid shoppingCartId);
        void DeleteItem(ShoppingCart shoppingCart, CarPart carPart);

        void SetQuantity(ShoppingCart shoppingCart, CarPart carPart, int quantity);

        Order Order(Client client);

      
    }
}

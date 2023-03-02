using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class ShoppingCartContainsCarPart
    {

        public ShoppingCartContainsCarPart() { }

        public ShoppingCartContainsCarPart(Guid carPartId, Guid shoppingCartId, CarPart carPart, ShoppingCart shoppingCart, int quantity)
        {
            CarPartId = carPartId;
            ShoppingCartId = shoppingCartId;
            CarPart = carPart;
            ShoppingCart = shoppingCart;
            Quantity = quantity;
        }

        public Guid CarPartId { get; set; }

        public Guid ShoppingCartId { get; set; }

      //  [ForeignKey("CarPartId")]
        public CarPart CarPart { get; set; }

       // [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }

        [Required] public int Quantity { get; set; }


    }

}

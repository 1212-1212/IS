using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class ShoppingCart : BaseEntity
    {

        public Client Client { get; set; }

        public string ClientId { get; set; }

        public virtual ICollection<ShoppingCartContainsCarPart> ShoppingCartContainsCarParts { get; set; }

      
    }
}

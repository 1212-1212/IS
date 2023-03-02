
using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EShop.Domain.Models
{
    public class OrderContainsCarPart
    {
        public OrderContainsCarPart() { }

        public OrderContainsCarPart(Guid carPartId, CarPart carPart, Order order, Guid orderId, int quantity)
        {
            CarPartId = carPartId;
            CarPart = carPart;
            Order = order;
            OrderId = orderId;
            Quantity = quantity;
        }

        public Guid CarPartId { get; set; }

        public CarPart CarPart { get; set; }

        public Order Order { get; set; }

        public Guid OrderId { get; set; }
       
        [Required] public int Quantity { get; set; }

        public override string ToString()
        {
            return String.Format("CarPart: ({0})\t Quantity: {1}\n", CarPart.ToString(), Quantity);
        }

    }
}

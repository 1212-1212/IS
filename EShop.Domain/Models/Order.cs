using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Domain.Models;

public class Order : BaseEntity
{
    public enum ORDER_STATUS
    {
        CREATED,
        PENDING,
        FINISHED,
    }
    
    public DateTime OrderDate { get; set; }



    public ORDER_STATUS OrderStatus { get; set; }

    public virtual ICollection<OrderContainsCarPart> OrderContainsCarParts { get; set; }  
     
    public string ClientId { get; set; }

  //  [ForeignKey("ClientUsername")]

    public Client Client { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(String.Format("Order ID: {0}\n", Id));
        for(int i=0;i<OrderContainsCarParts.Count();i++)
        {
            stringBuilder.Append(OrderContainsCarParts.ToList()[i].ToString());
        }
        var total = totalPrice();
        stringBuilder.Append(String.Format("Total: {0}", total));

        return stringBuilder.ToString();
    }

    public double totalPrice()
    {
        return OrderContainsCarParts.Sum(item => item.CarPart.Price * item.Quantity);
    }
}

using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models;

public class CarPartInStockAtDealership
{

    public Guid CarPartId { get; set; }

    public Guid DealershipId { get; set; }

    //[ForeignKey("CarPartId")]
    public CarPart CarPart { get; set; }

  //  [ForeignKey("DealershipId")]

    public Dealership Dealership { get; set; }
    [Required] public double Price { get; set; }
    [Required] public int Quantity { get; set; }




}
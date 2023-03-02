using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models;

public class Dealership : BaseEntity
{


    [Required] 
    public string Name { get; set; } = default!;
    [Required]
 //   public Guid AddressId { get; set; }
    
  //  [ForeignKey("AddressId")]
 //   public  Address Address { get; set; } = default!;

    public virtual IEnumerable<CarPartInStockAtDealership> CarPartInStockAtDealerships { get; set; }

}
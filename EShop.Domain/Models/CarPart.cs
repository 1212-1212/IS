using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EShop.Domain.Models;

public class CarPart : BaseEntity
{
    public CarPart() { }

    public CarPart(Guid carPartTypeId, CarPartType type, Guid carPartStageId, CarPartStage stage, string description, Guid carPartBrandId, CarPartBrand brand, double price)
    {
        CarPartTypeId = carPartTypeId;
        Type = type;
        CarPartStageId = carPartStageId;
        Stage = stage;
        Description = description;
        CarPartBrandId = carPartBrandId;
        Brand = brand;
        Price = price;
     
    }
    public CarPart(Guid carPartTypeId,  Guid carPartStageId, string description, Guid carPartBrandId, double price)
    {
        CarPartTypeId = carPartTypeId;
        CarPartStageId = carPartStageId;
        Description = description;
        CarPartBrandId = carPartBrandId;
        Price = price;

    }

    [Required]
    public Guid CarPartTypeId { get; set; }

    // [ForeignKey("CarPartTypeId")]
    [Required]
    public CarPartType Type { get; set; } = default!;
    
    [Required]
    public Guid CarPartStageId { get; set; }

    //  [ForeignKey("CarPartStageId")]
    [Required]
    public CarPartStage Stage { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public Guid CarPartBrandId { get; set; }

    //  [ForeignKey(("CarPartBrandId"))]

    [Required]
    public CarPartBrand Brand { get; set; } = default!;

    [Required]

    public double Price { get; set;}

    public virtual IEnumerable<CarPartInStockAtDealership> CarPartInStockAtDealerships { get; set; }

    public virtual IEnumerable<ShoppingCartContainsCarPart> ShoppingCartContainsCarParts { get; set;     }

    public virtual IEnumerable<OrderContainsCarPart> OrderContainsCarParts { get; set;}

    public override string ToString()
    {
        return String.Format("Description: {0}\tType: {1}\tStage: {2}\tBrand: {3}\tPrice: {4}\n",
           Description, Type.ToString(), Stage.ToString(), Brand.ToString(), Price);
    }

}
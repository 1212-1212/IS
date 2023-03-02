using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models;

public class CarPartBrand : BaseEntity
{


    [Required]
    public string BrandName { get; set; } = default!;

    [Required]
    public string Country { get; set; } = default!;

    public ICollection<CarPart> CarParts { get; set; }

    public override string ToString()
    {
        return BrandName;
    }


}
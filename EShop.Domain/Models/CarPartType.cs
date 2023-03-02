using System.ComponentModel.DataAnnotations;
using EShop.Domain.Models;

namespace EShop.Domain.Models;

public class CarPartType : BaseEntity
{
    
    [Required]
    public string Type { get; set; } = default!;
    public ICollection<CarPart> CarParts { get; set; }

    public override string ToString()
    {
        return Type;
    }

}
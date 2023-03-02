using System.ComponentModel.DataAnnotations;
using EShop.Domain.Models;

namespace EShop.Domain.Models;

public class CarPartStage : BaseEntity
{
 

    [Required] public string Stage { get; set; }

    public ICollection<CarPart> CarParts { get; set; }

    public override string ToString()
    {
        return Stage;
    }

}
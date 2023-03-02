using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models
{
    public class BaseEntity
    {

        [Key]
       
        public Guid Id { get; set; }
    }
}
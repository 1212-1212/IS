

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models;

public class Client : IdentityUser
{

        
    public Client() { }

    
    public Client(string name, string surname, ShoppingCart shoppingCart)
    {
  
        Name = name;
        Surname = surname;
        ShoppingCart = shoppingCart;
  
    }

   

    [Required]
    public string Name { get; set; }
    
        
    [Required]
    public string Surname { get; set; }

    // public Guid AddressId { get; set; }

    // [ForeignKey("AddressId")]

    //public Address Address { get; set; }

 
    public Guid ShoppingCartId { get; set; }

   // [ForeignKey("ShoppingCartId")]
    public ShoppingCart ShoppingCart { get; set; }


    public IEnumerable<Order> Orders { get; set; }

    
}
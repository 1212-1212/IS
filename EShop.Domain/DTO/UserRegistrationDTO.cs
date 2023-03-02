using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Surname { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
   
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}

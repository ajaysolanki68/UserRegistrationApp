using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApp.Models
{
    public class UserDto
    {
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        [MinLength(10)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        [Required]
        public int StateId { get; set; }  

        [Required]
        public int CityId { get; set; }   

    }
}

using System;
using System.ComponentModel.DataAnnotations;


namespace Sender.Models
{
    public class Register
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTimeOffset DateOfBirth { get; set; }
    }
}

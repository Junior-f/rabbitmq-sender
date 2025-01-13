using System;
using System.ComponentModel.DataAnnotations;

namespace Sender.Models
{
    public class ResetPassword
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string? ConfirmPassword { get; set; }
    }
}
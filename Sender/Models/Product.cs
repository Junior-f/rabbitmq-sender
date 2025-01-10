using System;
using System.ComponentModel.DataAnnotations;

namespace Sender.Models
{
    public class Product
    {
        [Required(ErrorMessage = "ID is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value is required")]
        public float Value { get; set; }
        [Required(ErrorMessage = "Qtd is required")]
        public int Qtd { get; set; }
    }
}
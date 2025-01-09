using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Sender.Models;
using Sender.Service;

namespace RabbitMQApp
{

    [ApiController]
    [Route("api/sender")]
    public class ProdutosController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        public static List<Product> Products { get; set; } = new List<Product>();
        public static List<Register> Registers { get; set; } = new List<Register>();
        public static List<ResetPassword> ResetPasswords { get; set; } = new List<ResetPassword>();

        public ProdutosController()
        {
            var hostname = "localhost";
            _rabbitMqService = new RabbitMqService(hostname);
        }

        [HttpPost("orders")]
        public async Task<IActionResult> IssuePurchaseOrder([FromBody] Product product)
        {
            if (product == null || product.Qtd <= 0 || product.Value <= 0)
            {
                return BadRequest("Dados do produto inválidos.");
            }

            Products.Add(product);
            await _rabbitMqService.SendMessage("ordem_de_compra", product);
            return Ok(product);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(Products);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Registers.Add(register);
            await _rabbitMqService.SendMessage("novo_cadastro", register);
            return Ok(register);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(Registers);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (string.IsNullOrWhiteSpace(resetPassword.Email) ||
                string.IsNullOrWhiteSpace(resetPassword.NewPassword) ||
                resetPassword.NewPassword != resetPassword.ConfirmPassword)
            {
                return BadRequest("Dados para redefinição de senha inválidos.");
            }
            ResetPasswords.Add(resetPassword);
            await _rabbitMqService.SendMessage("reset_senha", resetPassword);
            return Ok(resetPassword);
        }

        [HttpGet("reset-passwords")]
        public IActionResult GetResetPasswords()
        {
            return Ok(ResetPasswords);
        }
    }
}

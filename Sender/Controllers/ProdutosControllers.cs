using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Sender.Models;
using Sender.Service;

namespace Sender.Controllers
{

    [ApiController]
    [Route("api/sender")]
    public class ProdutosController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        private readonly ITemplateService _templateService;
        public static List<Product> Products { get; set; } = new List<Product>();
        public static List<Register> Registers { get; set; } = new List<Register>();
        public static List<ResetPassword> ResetPasswords { get; set; } = new List<ResetPassword>();
        public static List<TemplateMessageViewModel> TemplateMessageViewModels { get; set; } = new List<TemplateMessageViewModel>();

        public ProdutosController(IRabbitMqService rabbitMqService, ITemplateService templateService)
        {
            _rabbitMqService = rabbitMqService;
            _templateService = templateService;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> IssuePurchaseOrder([FromBody] Product product)
        {
            if (product == null || product.Qtd <= 0 || product.Value <= 0)
            {
                return BadRequest("Dados do produto inválidos.");
            }

            Products.Add(product);
            await _rabbitMqService.SendMessage("ordem_de_compra", "rota_compras", product);
            return Ok(product);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(Products);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TemplateMessageViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newRegister = _templateService.ToCreateMessageTemplateViewModel(register);

            TemplateMessageViewModels.Add(newRegister);
            await _rabbitMqService.SendMessage("medical_referral_tc_dev", "medical_referral_tc_dev", newRegister);
            return Ok(register);
        }

        [HttpPost("register2")]
        public async Task<IActionResult> Register2([FromBody] TemplateMessageViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newRegister = _templateService.ToCreateMessageTemplateViewModel(register);

            TemplateMessageViewModels.Add(newRegister);
            await _rabbitMqService.SendMessage("medical_referral_tc_dev", "medical_referral_tc_dev", newRegister);
            return Ok(register);
        }

        [HttpPost("register3")]
        public async Task<IActionResult> Register3([FromBody] TemplateMessageViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newRegister = _templateService.ToCreateMessageTemplateViewModel(register);

            TemplateMessageViewModels.Add(newRegister);
            await _rabbitMqService.SendMessage("medical_referral_tc_dev", "medical_referral_tc_dev", newRegister);
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
            await _rabbitMqService.SendMessage("reset_senha", "rota_senha", resetPassword);
            return Ok(resetPassword);
        }

        [HttpGet("reset-passwords")]
        public IActionResult GetResetPasswords()
        {
            return Ok(ResetPasswords);
        }

    }
}

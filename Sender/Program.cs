using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Sender.Models;
using Sender.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRabbitMqService>(sp => new RabbitMqService("localhost"));


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IConnection>(sp =>
// {
//     var factory = new ConnectionFactory() { HostName = "localhost" };
//     return factory.CreateConnection();
// });

//builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

// builder.Services.AddSingleton<IConnection>(sp =>
// {
//     var factory = new ConnectionFactory()
//     {
//         HostName = "localhost", // Substitua pelo seu hostname
//         UserName = "guest",     // Substitua pelo seu usuário
//         Password = "guest"      // Substitua pela sua senha
//     };
//     return factory.CreateConnectionAsync().Result;
// });

var app = builder.Build();

// Configuração do Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Mapeando os controllers
app.MapControllers();

app.Run();

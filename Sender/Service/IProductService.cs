using System;
using System.Collections.Generic;
using Sender.Models;

namespace Sender.Service
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        List<Product> GetAllProducts();
    }
}


using System;
using System.Collections.Generic;
using Sender.Models;
using Sender.Repositories;
using Sender.Service;





namespace Sender.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product CreateProduct(Product product)
        {
            if (product == null || product.Qtd <= 0 || product.Value <= 0)
            {
                throw new ArgumentException("Dados do produto inválidos.");
            }
            _productRepository.Add(product);
            return product;
        }
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
    }
}
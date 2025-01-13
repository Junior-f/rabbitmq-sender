using System;
using System.Collections.Generic;
using System.Linq;
using Sender.Models;

namespace Sender.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products;

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

    }
}
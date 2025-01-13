using System;
using Sender.Models;

namespace Sender.Repositories
{
    public interface IProductRepository
    {
        public void Add(Product pedido);
        List<Product> GetAll();

    



    }
}



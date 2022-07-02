using Inventory.Data;
using Inventory.Models;
using Inventory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public class ProductsViewModel
    {
        private ProductRepository _productRepository;

        public ProductsViewModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
    }
}

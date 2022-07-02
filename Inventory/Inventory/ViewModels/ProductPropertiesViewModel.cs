using Inventory.Models;
using Inventory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public class ProductPropertiesViewModel
    {
        private ProductPropertyRepository _productPropertyRepository;

        public ProductPropertiesViewModel(ProductPropertyRepository productPropertyRepository)
        {
            _productPropertyRepository = productPropertyRepository;
        }

        public List<ProductProperty> GetAllProperties()
        {
            return _productPropertyRepository.GetAll();
        }
    }
}

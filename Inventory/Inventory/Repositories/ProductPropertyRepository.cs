using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repositories
{
    public class ProductPropertyRepository
    {
        private DbSet<ProductProperty> _dbset;

        public ProductPropertyRepository(InventoryContext context)
        {
            _dbset = context.ProductProperties;
        }

        public List<ProductProperty> GetAll()
        {
            return _dbset.ToList();
        }
    }
}

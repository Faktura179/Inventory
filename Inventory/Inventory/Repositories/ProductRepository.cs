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
    public class ProductRepository
    {
        private DbSet<Product> _dbset;

        public ProductRepository(InventoryContext context)
        {
            _dbset = context.Products;
        }

        public List<Product> GetAll()
        {
            return _dbset.Include(x => x.ProductProperty).ToList();
        }

        public void Add(Product product)
        {
            _dbset.Add(product);
        }

        public void Remove(Product product)
        {
            _dbset.Remove(product);
        }

        public Product GetById(int productId)
        {
            return _dbset.Include(x => x.ProductProperty).FirstOrDefault(x => x.ProductId == productId);
        }
    }
}

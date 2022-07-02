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
    public class WarehouseProductRepository
    {
        private DbSet<WarehouseProduct> _dbset;

        public WarehouseProductRepository(InventoryContext context)
        {
            _dbset = context.WarehouseProducts;
        }

        public List<WarehouseProduct> GetAll()
        {
            return _dbset.ToList();
        }

        public void Add(WarehouseProduct product)
        {
            _dbset.Add(product);
        }

        internal List<WarehouseProduct> GetByWarehouseId(int warehouseId)
        {
            return _dbset.Include(x => x.Product).Where(x => x.WarehouseId == warehouseId).ToList();
        }

        public void Remove(WarehouseProduct product)
        {
            _dbset.Remove(product);
        }

        public WarehouseProduct GetById(int productId, int warehouseId)
        {
            return _dbset.Include(x => x.Product).FirstOrDefault(x => x.ProductId == productId && warehouseId == x.WarehouseId);
        }
    }
}

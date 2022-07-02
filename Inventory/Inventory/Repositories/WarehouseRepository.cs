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
    public class WarehouseRepository
    {
        private DbSet<Warehouse> _dbset;

        public WarehouseRepository(InventoryContext context)
        {
            _dbset = context.Warehouses;
        }

        public List<Warehouse> GetAll()
        {
            return _dbset.ToList();
        }
    }
}

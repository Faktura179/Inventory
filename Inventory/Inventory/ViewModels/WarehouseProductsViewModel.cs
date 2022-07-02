using Inventory.Models;
using Inventory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public class WarehouseProductsViewModel
    {
        private WarehouseProductRepository _warehouseProductRepository;

        public WarehouseProductsViewModel(WarehouseProductRepository warehouseProductRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
        }

        public List<WarehouseProduct> GetByWarehouseId(int warehouseId)
        {
            return _warehouseProductRepository.GetByWarehouseId(warehouseId);
        }
    }
}

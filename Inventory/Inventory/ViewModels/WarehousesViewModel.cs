using Inventory.Models;
using Inventory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public class WarehousesViewModel
    {
        private WarehouseRepository _warehouseRepository;

        public WarehousesViewModel(WarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public List<Warehouse> GetAllWarehouses()
        {
            return _warehouseRepository.GetAll();
        }
    }
}

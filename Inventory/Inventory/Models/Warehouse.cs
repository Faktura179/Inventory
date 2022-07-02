using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;

        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}

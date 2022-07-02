using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Product
    {
        public Product()
        {
            WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductPropertyId { get; set; }

        public virtual ProductProperty ProductProperty { get; set; } = null!;
        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}

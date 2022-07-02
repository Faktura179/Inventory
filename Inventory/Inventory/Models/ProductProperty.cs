using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class ProductProperty
    {
        public ProductProperty()
        {
            Products = new HashSet<Product>();
        }

        public int ProductPropertyId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class ProductProperty
    {
        public int ProductPropertyId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}

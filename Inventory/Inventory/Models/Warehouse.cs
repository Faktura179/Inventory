﻿using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Warehouse
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}

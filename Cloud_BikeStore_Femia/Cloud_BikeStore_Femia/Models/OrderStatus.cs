﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Cloud_BikeStore_Femia.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public byte StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

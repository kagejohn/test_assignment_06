﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestA6
{
    public class Order
    {
        public Pizza Pizza { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime PickupTime { get; set; }
        public bool Paid { get; set; }
        public bool Pickedup { get; set; }
    }
}

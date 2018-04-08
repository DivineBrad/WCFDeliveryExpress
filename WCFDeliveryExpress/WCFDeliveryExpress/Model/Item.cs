﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryExpress.Model
{
    public class Item
    {

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
        public ItemImage Image { get; set; }

    }
}

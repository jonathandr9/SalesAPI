﻿using System;
using System.Collections.Generic;

namespace SalesApi.Domain.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public Salesman Salesman { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public DateTime DateOfSale { get; set; }
        public SaleStatus SaleStatus { get; set; }
    }
}

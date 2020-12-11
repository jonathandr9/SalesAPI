using System;
using System.Collections.Generic;

namespace SalesApi.Domain.Models
{
    public class Sale
    {
        public Guid SaleID { get; set; } = Guid.NewGuid();
        public Salesman Salesman { get; set; }        
        public IEnumerable<Item> Items { get; set; }
        public DateTime DateOfSale { get; set; }
        public SaleStatus SaleStatus { get; set; }
    }
}

using System;

namespace SalesApi.Domain.Models
{
    public class Item
    {
        public Guid ItemID { get; private set; } = Guid.NewGuid();
        public Guid SaleID { get; set; }
        public string ItemName { get; set; }
    }
}

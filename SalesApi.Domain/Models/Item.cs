using System;

namespace SalesApi.Domain.Models
{
    public class Item
    {
        public Guid ItemID { get; private set; } = Guid.NewGuid();
        public string ItemName { get; set; }
    }
}

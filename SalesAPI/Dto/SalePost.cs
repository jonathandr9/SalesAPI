using System;
using System.Collections.Generic;

namespace SalesAPI.WebApi.Dto
{
    public class SalePost
    {
        public SalesmanPostDto Salesman { get; set; }
        public IEnumerable<ItemPostDto> Items { get; set; }
        public DateTime DateOfSale { get; set; }
    }
}

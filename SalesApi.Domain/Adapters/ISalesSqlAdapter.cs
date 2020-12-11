using SalesApi.Domain.Models;
using System;

namespace SalesApi.Domain.Adapters
{
    public interface ISalesSqlAdapter
    {
        Sale AddSale(Sale sale);
        Sale GetSaleByID(string SaleID);
        bool RemoveSale(Guid saleID);        
    }
}

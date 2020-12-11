using SalesApi.Domain.Models;

namespace SalesApi.Domain.Adapters
{
    public interface ISalesSqlAdapter
    {
        Sale AddSale(Sale sale);
        Sale GetSaleByID(string SaleID);
        bool RemoveSale(Sale sale);        
    }
}

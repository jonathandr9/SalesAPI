using SalesApi.Domain.Models;

namespace SalesApi.Domain.Adapters
{
    public interface ISalesSqlAdapter
    {
        void AddSale(Sale sale);
    }
}

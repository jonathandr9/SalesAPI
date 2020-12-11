using SalesApi.Domain.Models;

namespace SalesApi.Domain.Services
{
    public interface ISaleService
    {
        Sale RegisterSale(Sale sale);
        Sale SearchSale(string ID);
    }
}

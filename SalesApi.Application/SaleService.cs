using System;
using SalesApi.Domain.Adapters;
using SalesApi.Domain.Models;
using SalesApi.Domain.Services;
using SalesApi.Domain.Utils;

namespace SalesApi.Application
{
    public class SaleService : ISaleService
    {
        private readonly ISalesSqlAdapter _salesSqlAdapter;
        public SaleService(ISalesSqlAdapter salesSqlAdapter)
        {
            _salesSqlAdapter = salesSqlAdapter;
        }

        public Sale RegisterSale(Sale sale)
        {
            if (!Utils.IsAny(sale.Items))
                throw new Exception("Não é possível adicionar uma venda sem itens");

            if (sale.Salesman == null)
                throw new Exception("Não é possível adicionar uma venda sem os dados do vendedor");

            return _salesSqlAdapter.AddSale(sale);
        }

        public Sale SearchSale(string ID)
        {
            return _salesSqlAdapter.GetSaleByID(ID);
        }
    }
}

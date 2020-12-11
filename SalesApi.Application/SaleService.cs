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

        public Sale UpdateStatusSale(string ID, SaleStatus status)
        {
            var originalSale = _salesSqlAdapter.GetSaleByID(ID);

            if (originalSale == null)
                throw new Exception("Venda não encontrada");

            if (! checkStatusInUpdate(originalSale, status))
                throw new Exception("Status inválido");

            _salesSqlAdapter.RemoveSale(originalSale);
            originalSale.SaleStatus = status;            

            return  _salesSqlAdapter.AddSale(originalSale);
        }

        public bool checkStatusInUpdate(Sale originalSale, SaleStatus newStatus)
        {
            bool check = false;

            if (originalSale.SaleStatus == SaleStatus.AguardandoPagamento)
                check = (newStatus == SaleStatus.PagamentoAprovado || newStatus == SaleStatus.Cancelada);
            else if (originalSale.SaleStatus == SaleStatus.PagamentoAprovado)            
                check = (newStatus == SaleStatus.EnviadoParaTransportadora || newStatus == SaleStatus.Cancelada);            
            else
                check = (newStatus == SaleStatus.Entregue);

            return check;
        }
    }
}

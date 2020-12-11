using SalesApi.Domain.Adapters;
using SalesApi.Domain.Context;
using SalesApi.Domain.Models;
using System;
using System.Linq;

namespace SqlAdapter
{
    public class SalesSqlAdapter : ISalesSqlAdapter
    {
        private readonly DatabaseContext _context;

        public SalesSqlAdapter(DatabaseContext context)
        {
            _context = context;
        }       

        public Sale AddSale(Sale sale)
        {
            //Mapper
            sale.Salesman.SaleID = sale.SaleID;

            _context.Sales.Add(sale);

            foreach (var item in sale.Items)
                item.SaleID = sale.SaleID;            

            //Save database
            _context.Salesman.Add(sale.Salesman);
            _context.Items.AddRange(sale.Items);
            _context.SaveChanges();

            return sale;
        }

        public Sale GetSaleByID(string SaleID)
        {
            var sale =  _context.Sales
                                 .FirstOrDefault(s => s.SaleID.ToString() == SaleID);

            var salesman = _context.Salesman
                                .FirstOrDefault(s => s.SaleID.ToString() == SaleID);

            var items = _context.Items
                              .Where(s => s.SaleID.ToString() == SaleID)
                              .ToList();

            var response = new Sale()
            {
                SaleID = sale.SaleID,
                Salesman = salesman,
                Items = items,
                DateOfSale = sale.DateOfSale,
                SaleStatus = sale.SaleStatus
            };
            _context.SaveChanges();

            return response;
        }

        public bool RemoveSale(Guid saleID)
        {            
            var saleItemToRemove = 
                _context.Sales.SingleOrDefault(x => x.SaleID == saleID);

            _context.Sales.Remove(saleItemToRemove);
            _context.SaveChanges();

            return true;
        }
    }
}

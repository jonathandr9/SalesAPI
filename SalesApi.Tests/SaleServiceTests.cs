using SalesApi.Application;
using SalesApi.Domain.Adapters;
using SalesApi.Domain.Exceptions;
using SalesApi.Domain.Models;
using SalesApi.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace SalesApi.Tests
{
    public class SaleServiceTests
    {
        [Fact]
        [Trait("RegisterSale", "Sale")]
        public void RegisterSale_SaleService_Success()
        {
            //Arrange
            var items = new List<Item>()
            {
                new Item(){
                    ItemName = "Produto"
                }
            };

            Sale sale = new Sale()
            {
                SaleID = 1,
                Salesman = new Salesman()
                {
                    SalesmanID = 1,
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.AddSale(sale));
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act
            try
            {
                saleService.RegisterSale(sale);
            }

            //Assert
            catch (Exception)
            {
                Assert.False(true);
            }
            finally
            {
                Assert.True(true);
            }         
        }

        [Fact]
        [Trait("RegisterSale", "Sale")]
        public void RegisterSale_SaleService_ErrorWithoutItems()
        {
            //Arrange       
            Sale sale = new Sale()
            {
                SaleID = 1,
                Salesman = new Salesman()
                {
                    SalesmanID = 1,
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = null,
                DateOfSale = DateTime.Now
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.AddSale(sale));
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act / Assert
            Assert.Throws<Exception>(() => saleService.RegisterSale(sale));        
        }

        [Fact]
        [Trait("RegisterSale", "Sale")]
        public void RegisterSale_SaleService_ErrorWithoutSalesman()
        {
            //Arrange
            Sale sale = new Sale()
            {
                SaleID = 1,
                Salesman = null,
                Items = new List<Item>()
                {
                    new Item(){
                        ItemName = "Produto"
                    }
                },
                DateOfSale = DateTime.Now
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.AddSale(sale));
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act / Assert
            Assert.Throws<Exception>(() => saleService.RegisterSale(sale));
        }
    }
}

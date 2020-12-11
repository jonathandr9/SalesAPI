using Moq;
using SalesApi.Application;
using SalesApi.Domain.Adapters;
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
                Salesman = new Salesman()
                {                    
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
                
                Salesman = new Salesman()
                {                
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

        [Fact]
        [Trait("SearchSale", "Sale")]
        public void SearchSale_SaleService_Success()
        {
            //Arrange
            Guid saleIDExpected = Guid.NewGuid();

            var items = new List<Item>()
            {
                new Item(){
                    ItemName = "Produto"
                }
            };

            Sale sale = new Sale()
            {
                SaleID = saleIDExpected,
                Salesman = new Salesman()
                {
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.GetSaleByID(It.IsAny<string>())).Returns(sale);
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act
            var result = saleService.SearchSale(saleIDExpected.ToString());

            //Assert
            Assert.Equal(saleIDExpected, result.SaleID);
        }

        [Fact]
        [Trait("UpdateStatusSale", "Sale")]
        public void UpdateStatusSale_SaleService_Success()
        {
            //Arrange
            SaleStatus saleStatusExpected = SaleStatus.PagamentoAprovado;
            Guid saleIDExpected = Guid.NewGuid();

            var items = new List<Item>()
            {
                new Item(){
                    ItemName = "Produto"
                }
            };

            Sale originalSale = new Sale()
            {
                SaleID = saleIDExpected,
                Salesman = new Salesman()
                {
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now,
                SaleStatus = SaleStatus.AguardandoPagamento
            };

            Sale updatedSale = new Sale()
            {
                SaleID = saleIDExpected,
                Salesman = new Salesman()
                {
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now,
                SaleStatus = saleStatusExpected
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.GetSaleByID(It.IsAny<string>())).Returns(originalSale);
            sqlAdapterMoq.Setup(x => x.RemoveSale(It.IsAny<Guid>())).Returns(true);
            sqlAdapterMoq.Setup(x => x.AddSale(It.IsAny<Sale>())).Returns(updatedSale);
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act
            var result = saleService.UpdateStatusSale(saleIDExpected.ToString(), saleStatusExpected);

            //Assert
            Assert.Equal(saleStatusExpected, result.SaleStatus);
        }

        [Fact]
        [Trait("RegisterSale", "Sale")]
        public void RegisterSale_SaleService_ErrorInvalidStatus()
        {
            //Arrange       
            //Arrange
            SaleStatus saleStatusToUpdate = SaleStatus.Entregue;
            Guid saleIDExpected = Guid.NewGuid();

            var items = new List<Item>()
            {
                new Item(){
                    ItemName = "Produto"
                }
            };

            Sale originalSale = new Sale()
            {
                SaleID = saleIDExpected,
                Salesman = new Salesman()
                {
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now,
                SaleStatus = SaleStatus.AguardandoPagamento
            };

            Sale updatedSale = new Sale()
            {
                SaleID = saleIDExpected,
                Salesman = new Salesman()
                {
                    Cpf = "123.321.123-98",
                    Name = "Vendedor 1",
                    Email = "vendedor1@email.com",
                    Telefone = "31 65487-9876"
                },
                Items = items,
                DateOfSale = DateTime.Now,
                SaleStatus = saleStatusToUpdate
            };

            Moq.Mock<ISalesSqlAdapter> sqlAdapterMoq = new Moq.Mock<ISalesSqlAdapter>();
            sqlAdapterMoq.Setup(x => x.GetSaleByID(It.IsAny<string>())).Returns(originalSale);
            sqlAdapterMoq.Setup(x => x.RemoveSale(It.IsAny<Guid>())).Returns(true);
            sqlAdapterMoq.Setup(x => x.AddSale(It.IsAny<Sale>())).Returns(updatedSale);
            ISaleService saleService = new SaleService(sqlAdapterMoq.Object);

            //Act / Assert
            Assert.Throws<Exception>(() =>
                saleService.UpdateStatusSale(saleIDExpected.ToString(), saleStatusToUpdate));
        }
    }
}

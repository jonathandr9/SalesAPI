using System;

namespace SalesApi.Domain.Models
{
    public class Salesman
    {
        public Guid SalesmanID { get; private set; } = Guid.NewGuid();
        public Guid SaleID { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}

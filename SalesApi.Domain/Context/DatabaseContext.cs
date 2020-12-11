using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Models;

namespace SalesApi.Domain.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        {
        }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Salesman> Salesman { get; set; }
        public DbSet<Item> Items { get; set; }
    }


}

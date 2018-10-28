using Microsoft.EntityFrameworkCore;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserStocks> UserStocks { get; set; }
        public DbSet<StockPrices> StockPrices { get; set; }
    }
}
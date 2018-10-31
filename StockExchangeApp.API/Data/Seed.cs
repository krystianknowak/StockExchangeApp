using System.Linq;
using StockExchangeApp.API.Models;
using StockExchangeApp.API.Helpers;

namespace StockExchangeApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;   
        }

        public void SeedStockExchnage()
        {
            User stockExchnage = _context.Users.FirstOrDefault(s => s.Username.ToLower() == Extensions.STOCK_EXCHANGE);
            if(stockExchnage != null)
                return;
            
            byte[] passwordHash, passwordSalt;        
            Extensions.CreatePasswordHash(Extensions.STOCK_EXCHANGE, out passwordHash, out passwordSalt);    
            stockExchnage = new User();
            stockExchnage.Username = Extensions.STOCK_EXCHANGE;
            stockExchnage.PasswordHash = passwordHash;
            stockExchnage.PasswordSalt = passwordSalt;
            stockExchnage.FirstName = Extensions.STOCK_EXCHANGE;
            stockExchnage.LastName = Extensions.STOCK_EXCHANGE;
            stockExchnage.AvailableMoney = 0;
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "FP",
                    OwnedUnits = 100000000
                });
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "FPL",
                    OwnedUnits = 100000000
                });
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "PGB",
                    OwnedUnits = 100000000
                });
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "FPC",
                    OwnedUnits = 100000000
                });
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "FPA",
                    OwnedUnits = 100000000
                });
            stockExchnage.Stocks.Add(
                new UserStocks(){
                    CompanyCode = "DL24",
                    OwnedUnits = 100000000
                });

            _context.Users.AddAsync(stockExchnage);
            _context.UserStocks.AddRangeAsync(stockExchnage.Stocks);
            _context.SaveChangesAsync();
        }

    }
}
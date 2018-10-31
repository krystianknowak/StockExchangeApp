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
            
            stockExchnage = new User();
        }

    }
}
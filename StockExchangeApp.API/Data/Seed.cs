using System.Linq;
using StockExchangeApp.API.Models;

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
            User stockExchnage = _context.Users.FirstOrDefault(s => s.Username.ToLower() == "stockexchange");
            if(stockExchnage != null)
                return;
            
            stockExchnage = new User();
        }

    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetStockExchnage()
        {
            var stockExchnage = await _context.Users.Include(stocks => stocks.Stocks).FirstOrDefaultAsync(u => u.Username.ToLower() == "stockexchange");

            return stockExchnage;
        }

        public async Task<User> GetUser(int Id)
        {
            var user = await _context.Users.Include(stocks => stocks.Stocks).FirstOrDefaultAsync(u => u.Id == Id);
            
            return user;
        }
    }
}
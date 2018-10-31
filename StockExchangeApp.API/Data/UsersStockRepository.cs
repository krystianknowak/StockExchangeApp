using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public class UsersStockRepository : IUserStocksRepository
    {
        private readonly DataContext _context;
        public UsersStockRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<bool> BuyStock(UserStocks stock, decimal price)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == stock.UserId);
            
            var stockExchnage = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == "stockexchange");
            var stockExchnageStocks = await _context.UserStocks.FirstOrDefaultAsync(us => us.UserId == stockExchnage.Id && us.CompanyCode == stock.CompanyCode);
            if(stockExchnageStocks.OwnedUnits < stock.OwnedUnits){
                throw new Exception("Stock exchnage have too few units");
            }
            else{
                stockExchnageStocks.OwnedUnits -= stock.OwnedUnits;
            }

            if(stock.OwnedUnits * price > user.AvailableMoney){
                throw new Exception("You have too few money");
            }
            user.AvailableMoney -= stock.OwnedUnits * price;
            UserStocks checkingStock = await _context.UserStocks.Where(us => us.UserId == stock.UserId && us.CompanyCode == stock.CompanyCode).FirstOrDefaultAsync();
            if(checkingStock == null)
                await _context.UserStocks.AddAsync(stock);
            else{
                checkingStock.OwnedUnits += stock.OwnedUnits;
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SellStock(UserStocks stock, decimal price)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == stock.UserId);
            UserStocks checkingStock = await _context.UserStocks.Where(us => us.UserId == stock.UserId && us.CompanyCode == stock.CompanyCode).FirstOrDefaultAsync();
            if(checkingStock == null)
                throw new Exception("You do not have any units of this company");

            var stockExchnage = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == "stockexchange");
            var stockExchnageStocks = await _context.UserStocks.FirstOrDefaultAsync(us => us.UserId == stockExchnage.Id && us.CompanyCode == stock.CompanyCode);
            stockExchnageStocks.OwnedUnits += stock.OwnedUnits;

            if(stock.OwnedUnits > checkingStock.OwnedUnits){
                throw new Exception("You have too few units");
            }
            else if(stock.OwnedUnits == checkingStock.OwnedUnits){
                _context.UserStocks.Remove(checkingStock);
            }
            else{
                checkingStock.OwnedUnits -= stock.OwnedUnits;
            }

            user.AvailableMoney += stock.OwnedUnits * price;
            await _context.SaveChangesAsync();

            return true;
        }

        

    }
}
using System.Threading.Tasks;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public interface IUserStocksRepository
    {
        Task<bool> BuyStock(UserStocks stock, decimal price);
        Task<bool> SellStock(UserStocks stock, decimal price);
    }
}
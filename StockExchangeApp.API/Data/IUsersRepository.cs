using System.Threading.Tasks;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public interface IUsersRepository
    {
        Task<User> GetUser(int Id);

        Task<User> GetStockExchnage();
    }
}
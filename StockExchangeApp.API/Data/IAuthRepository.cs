using System.Threading.Tasks;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> RegisterWithStocks(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockExchangeApp.API.Models;
using StockExchangeApp.API.Helpers;

namespace StockExchangeApp.API.Data
{
public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            Extensions.CreatePasswordHash(password, out passwordHash, out passwordSalt); 

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        public async Task<User> RegisterWithStocks(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            Extensions.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var stockExchnage = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == Extensions.STOCK_EXCHANGE);
            var stockExchnageStocks = await _context.UserStocks.Where(us => us.UserId == stockExchnage.Id).ToListAsync();
            foreach(var s in user.Stocks)
            {
                var stockByCompanyCode = stockExchnageStocks.FirstOrDefault(x => x.CompanyCode == s.CompanyCode);
                if(s.OwnedUnits > stockByCompanyCode.OwnedUnits){
                    throw new Exception("Stock exchnage have too few units");
                }
                else{
                    var changedStock = await _context.UserStocks.FirstOrDefaultAsync(us => us.UserId == stockExchnage.Id && us.CompanyCode == s.CompanyCode);
                    changedStock.OwnedUnits -= s.OwnedUnits;
                }
            }

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.UserStocks.AddRangeAsync(user.Stocks);
            
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
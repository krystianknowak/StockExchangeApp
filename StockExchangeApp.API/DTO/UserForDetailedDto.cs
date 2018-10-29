using System.Collections.Generic;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.DTO
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AvailableMoney { get; set; }
        public ICollection<UserStocks> Stocks { get; set; }
    }
}
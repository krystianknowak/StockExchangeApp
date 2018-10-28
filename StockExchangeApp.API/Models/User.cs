using System.Collections.Generic;

namespace StockExchangeApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public byte[] Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AvailableMoney { get; set; }
        public ICollection<UserStocks> Stocks { get; set; }
    }
}
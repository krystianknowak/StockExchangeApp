namespace StockExchangeApp.API.Models
{
    public class UserStocks
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string CompanyCode { get; set; }
        public int OwnedUnits { get; set; }
    }
}
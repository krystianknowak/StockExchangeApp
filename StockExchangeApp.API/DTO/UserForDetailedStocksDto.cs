namespace StockExchangeApp.API.DTO
{
    public class UserForDetailedStocksDto
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public int OwnedUnits { get; set; }
    }
}
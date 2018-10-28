using System;

namespace StockExchangeApp.API.Models
{
    public class StockPrices
    {
        public int Id { get; set; }
        public decimal StockPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
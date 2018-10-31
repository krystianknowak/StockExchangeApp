using System;
using System.Collections.Generic;

namespace StockExchangeApp.API.Models
{
    public class CompanyUnitValue
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
    }

    public class FpResponse
    {
        public DateTime publicationDate { get; set; }
        public List<CompanyUnitValue> items { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.DTO
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 char")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public decimal AvailableMoney { get; set; }
        public ICollection<UserForDetailedStocksDto> Stocks { get; set; }
    }
}
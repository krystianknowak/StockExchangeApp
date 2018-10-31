using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockExchangeApp.API.Data;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IUserStocksRepository _repo;
        public StocksController(IUserStocksRepository repo)
        {
            _repo = repo;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> BuyStock(UserStocks stock)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);   
            stock.UserId = id;
            FpResponse unitsPrice = await GetUnitsValue();
            decimal unitPrice = RoundUp(unitsPrice.items.Select(i => i.Price).FirstOrDefault(), 2);
            var fpStockData = unitsPrice.items.FirstOrDefault(x => x.Code == stock.CompanyCode);
            if(fpStockData == null)
                return BadRequest("There is no company with this name");
            else if(stock.OwnedUnits % fpStockData.Unit != 0)
                return BadRequest("Wrong quantity of units");
            
            var buyStock = await _repo.BuyStock(stock, unitPrice);
            if(buyStock == false){
                return BadRequest("Wrong data!");
            }
            return Ok(true);  
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SellStock(UserStocks stock)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            stock.UserId = id;            
            FpResponse unitsPrice = await GetUnitsValue();
            decimal unitPrice = RoundDown(unitsPrice.items.Select(i => i.Price).FirstOrDefault(), 2);

            var fpStockData = unitsPrice.items.FirstOrDefault(x => x.Code == stock.CompanyCode);
            if(fpStockData == null)
                return BadRequest("There is no company with this name");
            else if(stock.OwnedUnits % fpStockData.Unit != 0)
                return BadRequest("Wrong quantity of units");
            
            var sellStock = await _repo.SellStock(stock, unitPrice);
            if(sellStock == false){
                return BadRequest("Wrong data!");
            }
            return Ok(true);  
        }


        private async Task<FpResponse> GetUnitsValue()
        {
            HttpClient client = new HttpClient();
            var responseString = await client.GetStringAsync("http://webtask.future-processing.com:8068/stocks");

            return JsonConvert.DeserializeObject<FpResponse>(responseString);
        }

        private decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }

        private decimal RoundUp(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Ceiling(i * power) / power;
        }
    }
}
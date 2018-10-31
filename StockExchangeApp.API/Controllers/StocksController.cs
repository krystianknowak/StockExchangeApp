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
using StockExchangeApp.API.Helpers;
using StockExchangeApp.API.DTO;

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


        [HttpPost("buystock")]
        public async Task<IActionResult> BuyStock(UserStocks stock)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);   
            stock.UserId = id;
            FpResponse unitsPrice = await Extensions.GetUnitsValue();
            decimal unitPrice = Extensions.RoundUp(unitsPrice.items.Select(i => i.Price).FirstOrDefault(), 2);
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

        [HttpPost("sellstock")]
        public async Task<IActionResult> SellStock(UserStocks stock)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            stock.UserId = id;            
            FpResponse unitsPrice = await Extensions.GetUnitsValue();
            decimal unitPrice = Extensions.RoundDown(unitsPrice.items.Select(i => i.Price).FirstOrDefault(), 2);

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

    }
}
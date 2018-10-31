using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockExchangeApp.API.Models;
using StockExchangeApp.API.Helpers;

namespace StockExchangeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FpController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getFpStockInformations()
        {
            return Ok(await Extensions.GetUnitsValue());
        } 
    }
}
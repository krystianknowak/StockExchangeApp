using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FpController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getFpStockInformations()
        {
            HttpClient client = new HttpClient();
            var responseString = await client.GetStringAsync("http://webtask.future-processing.com:8068/stocks");

            return Ok(JsonConvert.DeserializeObject<FpResponse>(responseString));
        } 
    }
}
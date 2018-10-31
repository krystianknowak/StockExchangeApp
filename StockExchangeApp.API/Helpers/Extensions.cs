using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Helpers {
    public static class Extensions {
        public static string STOCK_EXCHANGE = "stockexchange";
        public static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (password));
            }
        }

        public static async Task<FpResponse> GetUnitsValue()
        {
            HttpClient client = new HttpClient();
            var responseString = await client.GetStringAsync("http://webtask.future-processing.com:8068/stocks");

            return JsonConvert.DeserializeObject<FpResponse>(responseString);
        }

        public static decimal RoundDown (decimal i, double decimalPlaces) {
            var power = Convert.ToDecimal (Math.Pow (10, decimalPlaces));
            return Math.Floor (i * power) / power;
        }

        public static decimal RoundUp (decimal i, double decimalPlaces) {
            var power = Convert.ToDecimal (Math.Pow (10, decimalPlaces));
            return Math.Ceiling (i * power) / power;
        }
        public static void AddApplicationError (this HttpResponse response, string message) {
            response.Headers.Add ("Application-Error", message);
            response.Headers.Add ("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add ("Access-Control-Allow-Origin", "*");
        }
    }
}
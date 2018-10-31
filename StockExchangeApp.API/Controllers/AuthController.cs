using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StockExchangeApp.API.Data;
using StockExchangeApp.API.DTO;
using StockExchangeApp.API.Models;
using StockExchangeApp.API.Helpers;

namespace StockExchangeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // validate request
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username alredy exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                AvailableMoney = userForRegisterDto.AvailableMoney
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("withstocks")]
        public async Task<IActionResult> RegisterWithStocks(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username alredy exists");

            FpResponse companyUnitValues = await Extensions.GetUnitsValue();
            List<UserStocks> stockToAdd = new List<UserStocks>();
            foreach(var stock in userForRegisterDto.Stocks)
            {
                var companyUnitValue = companyUnitValues.items.Find(x => x.Code == stock.CompanyCode);
                if(companyUnitValue == null || !(stock.OwnedUnits % companyUnitValue.Unit == 0))
                    return BadRequest(String.Format("Quantity of {0} must be multiple of {1}", stock.CompanyCode, companyUnitValue.Unit));
                
                stockToAdd.Add(new UserStocks(){
                    CompanyCode = stock.CompanyCode,
                    OwnedUnits = stock.OwnedUnits
                });
            }

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                AvailableMoney = Extensions.RoundDown(userForRegisterDto.AvailableMoney, 2),
                Stocks = stockToAdd
            };
            
            var createdUser = await _repo.RegisterWithStocks(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if(userFromRepo == null || userFromRepo.Username.ToLower() == Extensions.STOCK_EXCHANGE)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
                new Claim(ClaimTypes.Surname, userFromRepo.FirstName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
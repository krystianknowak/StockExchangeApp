using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchangeApp.API.Data;
using StockExchangeApp.API.DTO;

namespace StockExchangeApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpGet("stockexchnage")]
        public async Task<IActionResult> GetStockExchnage()
        {
            var StockExchange = await _repo.GetStockExchnage();
            var stockExchnageToReturn = _mapper.Map<UserForDetailedDto>(StockExchange);

            return Ok(stockExchnageToReturn);
        }

    }
}
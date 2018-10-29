using AutoMapper;
using StockExchangeApp.API.DTO;
using StockExchangeApp.API.Models;

namespace StockExchangeApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>();
        }
    }
}
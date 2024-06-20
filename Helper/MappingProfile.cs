using AtmEquityProject.Dto;
using AtmEquityProject.Models;
using AutoMapper;

namespace AtmEquityProject.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Atm, AtmDto>();
            CreateMap<AtmDto, Atm>();
            CreateMap<Balance, BalanceDto>();
            CreateMap<BalanceDto, Balance>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}

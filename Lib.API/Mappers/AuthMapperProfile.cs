using AutoMapper;
using Lib.API.DTOs.Auth;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Mappers
{
    public class AuthMapperProfile : Profile
    {
        public AuthMapperProfile()
        {
            CreateMap<RegisterDTO, RegisterRequest>();
            CreateMap<LoginDTO, LoginRequest>();
        }
    }
}

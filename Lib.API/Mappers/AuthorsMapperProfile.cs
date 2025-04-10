using AutoMapper;
using Lib.API.DTOs.Authors;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Mappers
{
    public class AuthorsMapperProfile : Profile
    {
        public AuthorsMapperProfile()
        {
            CreateMap<AddAuthorDTO, AddAuthorRequest>();
            CreateMap<UpdateAuthorDTO, UpdateAuthorRequest>();
        }
    }
}

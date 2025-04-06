using AutoMapper;
using Lib.Core.Entities;
using Lib.Application.Models;

namespace Lib.Application.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AuthorEntity, Author>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}

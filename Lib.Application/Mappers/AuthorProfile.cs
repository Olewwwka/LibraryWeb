using AutoMapper;
using Lib.Core.Entities;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Books,
                     opt => opt.MapFrom(src => src.Books));

            CreateMap<AuthorEntity, Author>()
                .ForMember(dest => dest.Books,
                    opt => opt.MapFrom(src => src.Books));
        }
    }
}

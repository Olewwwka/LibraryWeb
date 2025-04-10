﻿using AutoMapper;
using Lib.Core.Entities;
using Lib.Application.Models;
using Lib.Application.Contracts.Requests;

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

            CreateMap<UpdateAuthorRequest, AuthorEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Books, opt => opt.Ignore());

        }
    }
}

using AutoMapper;
using Lib.Core.Entities;
using Lib.Application.Models;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookEntity>()
              .ForMember(dest => dest.Id, opt => opt.Ignore()) 
              .ForMember(dest => dest.Author, opt => opt.Ignore())
              .ForMember(dest => dest.User, opt => opt.Ignore())
              .ForMember(dest => dest.BorrowTime,
                  opt => opt.MapFrom(src => src.BorrowTime ?? DateTime.MinValue))
              .ForMember(dest => dest.ReturnTime,
                  opt => opt.MapFrom(src => src.ReturnTime ?? DateTime.MinValue))
              .ForMember(dest => dest.ImagePath,
                  opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ImagePath) ? "default_image.jpg" : src.ImagePath));

            CreateMap<BookEntity, Book>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
              .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId.ToString()))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.BorrowTime, opt => opt.MapFrom(src => src.BorrowTime))
              .ForMember(dest => dest.ReturnTime, opt => opt.MapFrom(src => src.ReturnTime))
              .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
              .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath));

            CreateMap<UpdateBookInfoRequest, BookEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => "default_image.jpg"))
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using Lib.Core.Entities;
using Lib.Application.Models;

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
                  opt => opt.MapFrom(src => src.ReturnTime ?? DateTime.MinValue));

            CreateMap<BookEntity, Book>()
              .ConstructUsing(src => new Book(
                    src.ISBN,
                    src.Name,
                    src.Genre,
                    src.Description,
                    src.AuthorId))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.BorrowTime, opt => opt.MapFrom(src => src.BorrowTime))
              .ForMember(dest => dest.ReturnTime, opt => opt.MapFrom(src => src.ReturnTime));
        }
    }
}

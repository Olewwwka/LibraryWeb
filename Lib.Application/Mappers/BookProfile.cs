using AutoMapper;
using Lib.Core.Entities;
using Lib.Core.Models;

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
                .ForMember(dest => dest.IsBorrowed, opt => opt.Ignore())
                .ForMember(dest => dest.IsOverdue, opt => opt.Ignore());
        }
    }
}

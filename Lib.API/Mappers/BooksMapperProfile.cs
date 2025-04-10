using AutoMapper;
using Lib.API.DTOs.Books;
using Lib.API.DTOs.Users;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Mappers
{
    public class BooksMapperProfile : Profile
    {
        public BooksMapperProfile()
        {
            CreateMap<AddBookDTO, AddBookRequest>();
            CreateMap<BorrowBookDTO, BorrowBookRequest>();
        }
    }
}

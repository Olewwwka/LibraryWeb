using AutoMapper;
using Lib.Application.Models;
using Lib.Core.Entities;

namespace Lib.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)) 
                .ForMember(dest => dest.BorrowedBooks, opt => opt.Ignore()); 

            CreateMap<UserEntity, User>()
                .ConstructUsing(src => new User(
                    src.Id,
                    src.Name,
                    src.Email,
                    src.PasswordHash,
                    src.Role));

            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.PasswordHash)));
        }
    }
}

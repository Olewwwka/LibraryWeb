using AutoMapper;
using Lib.Core.DTOs;
using Lib.Core.Entities;

namespace Lib.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User")) 
                .ForMember(dest => dest.BorrowedBooks, opt => opt.Ignore()); 

            CreateMap<UserEntity, User>()
                .ConstructUsing(src => new User(
                    src.Name,
                    src.Email,
                    src.PasswordHash));

            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.PasswordHash)));
        }
    }
}

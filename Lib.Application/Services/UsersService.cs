using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.DTOs;
using Lib.Core.Entities;
using System.Security.Claims;

namespace Lib.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public UsersService(ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<User> Register(string name, string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email, cancellationToken);

            if (existingUser != null) throw new Exception();  //========================= 

            var passwordHash = _passwordHasher.Generate(password);

            var user = new User(name, email, passwordHash);
            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddUserAsync(userEntity, cancellationToken);

            return user;
        }

        public async Task<(User, string accessToken, string RefreshToken)> Login(string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email, cancellationToken);
            if (existingUser == null) throw new Exception();  //========================= 

            if (!_passwordHasher.Verify(password, existingUser.PasswordHash)) throw new Exception();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new(ClaimTypes.Email, existingUser.Email),
                new(ClaimTypes.Role, existingUser.Role)
            };

            var jwtToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _tokenService.StoreRefreshTokenAsync(existingUser.Id, refreshToken);

            var user = _mapper.Map<User>(existingUser);

            return (user, jwtToken, refreshToken);
        }

    }
}

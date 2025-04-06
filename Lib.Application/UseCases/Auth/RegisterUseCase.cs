using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using Lib.Core.Entities;
using Lib.Core.Exceptions;


namespace Lib.Application.UseCases.Auth
{
    public class RegisterUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public RegisterUseCase(ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<User> ExecuteAsync(string name, string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email, cancellationToken);

            if (existingUser != null)
            { 
                throw new UserAlreadyExistsException("User with current email already exists"); 
            }

            var passwordHash = _passwordHasher.Generate(password);

            var user = new User(Guid.NewGuid(), name, email, passwordHash, "Admin");
            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddUserAsync(userEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}

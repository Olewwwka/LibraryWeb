using AutoMapper;
using Lib.Application.Models;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Abstractions.Services;
using Lib.Core.Abstractions;
using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Application.Abstractions;



namespace Lib.Application.UseCases.Auth
{
    public class RegisterUseCase : IRegisterUseCase
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

        public async Task<RegisterResponse> ExecuteAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (existingUser != null)
            { 
                throw new UserAlreadyExistsException("User with current email already exists"); 
            }

            var passwordHash = _passwordHasher.Generate(request.Password);

            var user = new User(Guid.NewGuid(), request.Name, request.Email, passwordHash, "User");
            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddUserAsync(userEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterResponse(
                userEntity.Id,
                userEntity.Name,
                userEntity.Email,
                userEntity.Role
                );
        }

    }
}

using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.DTOs;
using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (existingUser != null) throw new Exception();  //========================= 

            var passwordHash = _passwordHasher.Generate(password);

            var user = new User(name, email, passwordHash);
            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddUserAsync(userEntity, cancellationToken);

            return user;
        }
    }
}

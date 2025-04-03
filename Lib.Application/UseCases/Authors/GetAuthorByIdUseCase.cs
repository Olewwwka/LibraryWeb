using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.UseCases.Authors
{
    public class GetAuthorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var authorEntity = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);
            var author = _mapper.Map<Author>(authorEntity);

            return author;
        }
    }
}

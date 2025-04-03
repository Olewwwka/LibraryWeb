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
    public class GetAllAuthorsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllAuthorsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Author>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var authorEntities = await _unitOfWork.AuthorsRepository.GetAllAuthorsAsync(cancellationToken);
            var authors = _mapper.Map<List<Author>>(authorEntities);

            return authors;
        }
    }
}

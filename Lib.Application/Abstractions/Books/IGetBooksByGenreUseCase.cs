﻿using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Books
{
    public interface IGetBooksByGenreUseCase : IUseCase<GetBooksByGenreRequest, GetAllBooksResponse> { }
}

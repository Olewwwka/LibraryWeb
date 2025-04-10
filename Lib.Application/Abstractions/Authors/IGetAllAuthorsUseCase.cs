﻿using Lib.Application.Models;
using Lib.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Abstractions.Authors
{
    public interface IGetAllAuthorsUseCase : IUseCase<List<Author>> {}
}

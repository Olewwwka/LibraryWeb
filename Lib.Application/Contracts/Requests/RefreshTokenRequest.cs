﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Requests
{
    public record RefreshTokenRequest
    (
        string OldAccessToken,
        string OldRefreshToken
    );
}

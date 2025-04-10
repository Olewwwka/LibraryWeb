﻿namespace Lib.Core.Abstractions.Services
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool Verify(string password, string passworHash);

    }
}

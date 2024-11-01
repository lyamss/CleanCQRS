﻿namespace Application.Services
{
    internal sealed class ConfigString : IConfigString
    {
        public string KeyInCacheGetByIdAsync => "GetByIdAsync";

        public string KeyInCacheGetUserWithPseudo => "GetUserWithPseudo";

        public string KeyInCacheGetAllUsers => "GetAllUsers";
    }
}
﻿using NAF.Domain.Enum;
using NAF.Domain.ValueObjects;
using System.IdentityModel.Tokens.Jwt;

namespace NAF.Domain.Interface.Services
{
    public interface IJwtService
    {
        UserToken BuildToken(Guid codigoUsuario, string email, TipoPerfil tipoPerfil, bool primeiroLogin = false);
        JwtSecurityToken ValidateToken(string jwtToken);
    }
}

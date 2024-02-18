using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NAF.Domain.Enum;
using NAF.Domain.Interface.Services;
using NAF.Domain.ValueObjects;
using NAF.Infra.Data.Extensions;
using NAF.Infra.Data.External_Dependence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NAF.Domain.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;

        public JwtService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public UserToken BuildToken(Guid codigoUsuario, string email, TipoPerfil tipoPerfil, bool primeiroLogin = false)
        {
            var roles = GetRoles(tipoPerfil);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, codigoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, email),
                new Claim("PrimeiroLogin", primeiroLogin.ToString())

            };

            claims.AddRange(roles.Select(s => new Claim(ClaimsIdentity.DefaultRoleClaimType, s)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT!.Key!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new(
               issuer: _appSettings.JWT!.Issuer!,
               audience: _appSettings.JWT!.Audience!,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                CodigoUsuario = codigoUsuario,
                Email = email,
                TipoPerfil = tipoPerfil,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        public JwtSecurityToken ValidateToken(string jwtToken)
        {

            IdentityModelEventSource.ShowPII = true;

            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = _appSettings.JWT!.Audience!,
                ValidIssuer = _appSettings.JWT!.Issuer!,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT!.Key!))
            };

            new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out var validatedToken);

            return validatedToken as JwtSecurityToken;
        }

        private IEnumerable<string> GetRoles<T>(T source) where T : System.Enum
        {
            var enumValues = EnumExtensions.GetValues<T>();

            var roles = (from value in enumValues where source.HasFlag(value) select value.ToString()).ToList();

            return roles;
        }
    }
}

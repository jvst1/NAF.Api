using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        public UserToken BuildToken<T>(Guid codigoUsuario, string email, T perfil) where T : System.Enum
        {
            var roles = GetRoles(perfil);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, codigoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, email),
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
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        private IEnumerable<string> GetRoles<T>(T source) where T : System.Enum
        {
            var enumValues = EnumExtensions.GetValues<T>();

            var roles = (from value in enumValues where source.HasFlag(value) select value.ToString()).ToList();

            return roles;
        }
    }
}

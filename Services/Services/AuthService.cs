using Microsoft.AspNetCore.Identity;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<UserToken> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Name);

            if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                throw new Exception("Login inválido.");

            return _jwtService.BuildToken(user.Email);
        }
    }
}

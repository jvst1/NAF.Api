using Microsoft.AspNetCore.Identity;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthService(UserManager<IdentityUser> userManager, IJwtService jwtService, IUserService userService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userService = userService;
        }

        public async Task<UserToken> Login(LoginRequest request)
        {
            var identityUser = await _userManager.FindByNameAsync(request.DocumentoFederal);

            if (!await _userManager.CheckPasswordAsync(identityUser, request.Password))
                throw new Exception("Login inválido.");

            var user = _userService.GetUserByDocumentoFederal(request.DocumentoFederal!);
            return _jwtService.BuildToken(user.Codigo, user.Email, user.TipoPerfil.GetValueOrDefault());
        }
    }
}

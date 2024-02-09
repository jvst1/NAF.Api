using NAF.Application.Interfaces;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IAuthService _authService;

        public AuthAppService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<UserToken> Login(LoginRequest request) => await _authService.Login(request);
    }
}

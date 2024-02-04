using Microsoft.AspNetCore.Identity;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;

        public UserService(UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<UserToken> CreateUser(CreateUserRequest userRequest)
        {
            var user = new IdentityUser
            {
                UserName = userRequest.Name,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (!result.Succeeded)
                throw new Exception($"Erro ao registrar usuário: {result.Errors}");

            return _jwtService.BuildToken(user.Email!);
        }
    }
}

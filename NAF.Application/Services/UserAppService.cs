using NAF.Application.Interfaces;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserToken> CreateUser(CreateUserRequest createUserRequest)
        {
            return await _userService.CreateUser(createUserRequest);
        }
    }
}

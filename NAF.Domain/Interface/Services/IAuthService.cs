using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IAuthService
    {
        public Task<UserToken> Login(LoginRequest loginRequest);
    }
}

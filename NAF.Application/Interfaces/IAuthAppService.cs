using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Interfaces
{
    public interface IAuthAppService
    {
        public Task<UserToken> Login(LoginRequest request);
    }
}

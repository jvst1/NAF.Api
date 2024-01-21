using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IUserService
    {
        public Task<UserToken> CreateUser(CreateUserRequest userRequest);
    }
}

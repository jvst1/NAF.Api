using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserToken> CreateUser(CreateUserRequest createUserRequest);
    }
}

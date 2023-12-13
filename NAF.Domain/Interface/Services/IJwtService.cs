using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IJwtService
    {
        UserToken BuildToken(string email);
    }
}

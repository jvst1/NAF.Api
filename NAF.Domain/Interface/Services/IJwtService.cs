using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IJwtService
    {
        UserToken BuildToken<T>(Guid codigoUsuario, string email, T perfil) where T : System.Enum;
    }
}

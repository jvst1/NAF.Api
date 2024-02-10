using NAF.Domain.Enum;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IJwtService
    {
        UserToken BuildToken(Guid codigoUsuario, string email, TipoPerfil tipoPerfil);
    }
}

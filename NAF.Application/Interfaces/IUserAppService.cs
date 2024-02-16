using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil tipoPerfil);
        Usuario GetUserByCodigo(Guid id);
        List<Usuario> GetAllUsuarioOperador();
    }
}

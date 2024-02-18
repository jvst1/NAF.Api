using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil tipoPerfil);
        void RecuperarSenha(RecuperarSenhaRequest request);
        void SolicitarLinkSenha(SolicitarLinkSenhaRequest request);
        Usuario GetUserByCodigo(Guid id);
        List<Usuario> GetAllUsuarioOperador();
    }
}

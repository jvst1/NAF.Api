using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Interface.Services
{
    public interface IUserService
    {
        public Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil tipoPerfil);
        Usuario? GetUserByCodigo(Guid codigoUsuario);
        Usuario? GetUserByEmail(string email);
        Usuario? GetUserByDocumentoFederal(string documentoFederal);
        List<Usuario> GetAllUsuarioOperador();
    }
}

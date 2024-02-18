using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService, IUsuarioRepository usuarioRepository)
        {
            _userService = userService;
        }

        public async Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil tipoPerfil)
        {
            return await _userService.CreateUser(request, tipoPerfil);
        }

        public void RecuperarSenha(RecuperarSenhaRequest request)
        {
            _userService.RecuperarSenha(request);
        }

        public void SolicitarLinkSenha(SolicitarLinkSenhaRequest request)
        {
            _userService.SolicitarLinkSenha(request);
        }

        public Usuario GetUserByCodigo(Guid id)
        {
            return _userService.GetUserByCodigo(id);
        }

        public List<Usuario> GetAllUsuarioOperador()
        {
            return _userService.GetAllUsuarioOperador();
        }
    }
}

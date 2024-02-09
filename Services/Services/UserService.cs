using Microsoft.AspNetCore.Identity;
using NAF.Domain.Base;
using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using NAF.Domain.ValueObjects;

namespace NAF.Domain.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UserService(UserManager<IdentityUser> userManager, IJwtService jwtService, IUsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil perfil)
        {
            if (!Util.ValidaDocumento(request.DocumentoFederal))
                throw new ArgumentException("O documento federal informado não é válido. Revise as informações e tente novamente");

            request.DocumentoFederal = Util.DeixaNumeros(request.DocumentoFederal);
            Usuario? entity;

            entity = _usuarioRepository.GetByEmail(request.Email);
            if (entity != null)
                throw new InvalidOperationException("O email informado já está sendo utilizado por outro usuário.");

            entity = _usuarioRepository.GetByDocumentoFederal(request.DocumentoFederal);
            if (entity != null)
                throw new InvalidOperationException("O documento federal informado já está sendo utilizado por outro usuário.");

            var user = new IdentityUser
            {
                UserName = request.DocumentoFederal,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception($"Erro ao registrar usuário: {result.Errors}");

            var identityUser = _userManager.FindByEmailAsync(user.Email);

            entity = new Usuario
            {
                Codigo = Guid.NewGuid(),
                DtInclusao = DateTime.Now,
                Email = request.Email,
                DocumentoFederal = request.DocumentoFederal,
                Nome = request.Name,
                TipoPerfil = perfil,
                Situacao = SituacaoUsuario.Ativo,
                Identificador = request.Apelido,
                TelefoneCelular = request.PhoneNumber,
                IdentityUserId = identityUser.Result.Id,
                IdentityUser = identityUser.Result
            };

            _usuarioRepository.Insert(entity);
            _usuarioRepository.SaveChanges();

            return _jwtService.BuildToken(entity.Codigo, entity.Email, entity.TipoPerfil.GetValueOrDefault());
        }

        public Usuario? GetUserByCodigo(Guid codigoUsuario)
        {
            return _usuarioRepository.GetByCodigo(codigoUsuario);
        }

        public Usuario? GetUserByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        public Usuario? GetUserByDocumentoFederal(string documentoFederal)
        {
            return _usuarioRepository.GetByDocumentoFederal(documentoFederal);
        }
    }
}

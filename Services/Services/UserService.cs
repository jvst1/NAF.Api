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
        private readonly IEnvioEmailService _envioEmailService;

        public UserService(UserManager<IdentityUser> userManager,
                           IJwtService jwtService,
                           IUsuarioRepository usuarioRepository,
                           IEnvioEmailService envioEmailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
            _envioEmailService = envioEmailService;
        }

        public async Task<UserToken> CreateUser(CreateUserRequest request, TipoPerfil perfil)
        {
            if (!Util.ValidaDocumento(request.DocumentoFederal))
                throw new ArgumentException("O documento federal informado não é válido. Revise as informações e tente novamente");

            request.DocumentoFederal = Util.DeixaNumeros(request.DocumentoFederal);
            Usuario? entity;

            entity = _usuarioRepository.GetByEmail(request.Email);
            if (entity != null)
                throw new InvalidOperationException("O email informado já está sendo utilizado por outro usuário");

            entity = _usuarioRepository.GetByDocumentoFederal(request.DocumentoFederal);
            if (entity != null)
                throw new InvalidOperationException("O documento federal informado já está sendo utilizado por outro usuário");

            var user = new IdentityUser
            {
                UserName = request.DocumentoFederal,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            if (request.TipoPerfil.Equals(TipoPerfil.Aluno) || request.TipoPerfil.Equals(TipoPerfil.Professor))
                request.Password = Util.CriarSenha();

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

            if (request.TipoPerfil.Equals(TipoPerfil.Aluno) || request.TipoPerfil.Equals(TipoPerfil.Professor))
            {
                entity.Situacao = SituacaoUsuario.Inativo;
                var solicitarLinkSenhaRequest = new SolicitarLinkSenhaRequest { Login = entity.DocumentoFederal, PrimeiroAcesso = true };

                SolicitarLinkSenha(solicitarLinkSenhaRequest);
            }

            _usuarioRepository.Insert(entity);
            _usuarioRepository.SaveChanges();

            return _jwtService.BuildToken(entity.Codigo, entity.Email, entity.TipoPerfil.GetValueOrDefault());
        }

        public void UpdateUserProfile(UpdatePerfilUsuario request, Usuario usuario)
        {
            if (usuario.Nome != request.Nome)
                usuario.Nome = request.Nome;

            if (usuario.Email != request.Email)
            {
                if (_usuarioRepository.GetByEmail(request.Email!)?.Codigo == usuario.Codigo)
                    usuario.Email = request.Email;
                else
                    throw new Exception("Esse e-mail não está disponível, tente outro e-mail");
            }

            if (usuario.Identificador != request.Identificador)
                usuario.Identificador = request.Identificador;

            if (usuario.TelefoneCelular != request.TelefoneCelular)
                usuario.TelefoneCelular = request.TelefoneCelular;

            if (usuario.TipoPerfil != request.TipoPerfil)
                usuario.TipoPerfil = request.TipoPerfil;

            _usuarioRepository.Update(usuario);
            _usuarioRepository.SaveChanges();
        }

        public void RecuperarSenha(RecuperarSenhaRequest request)
        {
            var validatedToken = _jwtService.ValidateToken(request.Token);

            if (validatedToken == null)
                throw new InvalidDataException("O token fornecido é inválido ou não pôde ser validado.");

            if (!Guid.TryParse(validatedToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value, out var codigoUsuario))
                throw new InvalidOperationException("Não foi possível obter o código do usuário a partir do token.");

            var primeiraVez = validatedToken.Claims.FirstOrDefault(c => c.Type == "PrimeiroLogin")?.Value == "true";
            var usuario = _usuarioRepository.GetByCodigo(codigoUsuario);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado na base de dados.");

            if (!string.IsNullOrWhiteSpace(request.Login) && !Util.ValidaDocumento(request.Login))
                throw new ArgumentException("O documento federal informado é inválido");

            request.Login = Util.DeixaNumeros(request.Login);

            if (usuario.Codigo != request.CodigoUsuario || usuario.DocumentoFederal != request.Login)
                throw new InvalidDataException("As informações fornecidas não correspondem às registradas no sistema.");

            if (usuario.Situacao.GetValueOrDefault() == SituacaoUsuario.Ativo && primeiraVez)
                throw new InvalidOperationException("O cadastro deste usuário já está ativo. Recupere sua senha.");

            var usuarioIdentity = _userManager.FindByIdAsync(usuario.IdentityUserId).Result;

            Util.ValidaSenha(request.NovaSenha);

            _userManager.RemovePasswordAsync(usuarioIdentity);
            _userManager.AddPasswordAsync(usuarioIdentity, request.NovaSenha);

            if (primeiraVez)
            {
                usuario.Situacao = SituacaoUsuario.Ativo;

                _usuarioRepository.Update(usuario);
                _usuarioRepository.SaveChanges();
            }
        }

        public void SolicitarLinkSenha(SolicitarLinkSenhaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login))
                throw new ArgumentException("É obrigatório que seja informado um documento federal");

            if (!string.IsNullOrWhiteSpace(request.Login) && !Util.ValidaDocumento(request.Login))
                throw new ArgumentException("O documento federal informado para login é inválido");

            var usuario = _usuarioRepository.GetByDocumentoFederal(request.Login);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            if (usuario.Situacao == SituacaoUsuario.Inativo)
                throw new KeyNotFoundException("O usuário está inativo e não pode solicitar redefinição de senha.");

            if (usuario.Situacao.GetValueOrDefault(0) == SituacaoUsuario.Ativo && request.PrimeiroAcesso)
                throw new InvalidOperationException("O cadastro já foi ativado. Se você esqueceu sua senha, use a opção de redefinição de senha.");

            var token = _jwtService.BuildToken(usuario.Codigo, usuario.Email!, usuario.TipoPerfil.GetValueOrDefault(), request.PrimeiroAcesso).Token;

            var email = _envioEmailService.RegistrarEmailRecuperarSenha(usuario, token!);
            _envioEmailService.SendAsync(email).GetAwaiter().GetResult();
        }

        public List<Usuario> GetAllUsuarioOperador()
        {
            List<Usuario> operadores = _usuarioRepository.GetAll()
                                     .Where(o => o.TipoPerfil.Equals(TipoPerfil.Aluno) || o.TipoPerfil.Equals(TipoPerfil.Professor))
                                     .ToList();

            return operadores;
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

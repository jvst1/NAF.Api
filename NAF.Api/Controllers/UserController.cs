using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Enum;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class UserController : NafControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService) : base(userAppService)
        {
            _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            try
            {
                var result = await _userAppService.CreateUser(request, TipoPerfil.Comunidade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        [HttpPost("Operador/register")]
        public async Task<IActionResult> RegisterOperador(CreateUserRequest request)
        {
            try
            {
                var result = await _userAppService.CreateUser(request, request.TipoPerfil);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        [HttpGet("Operador")]
        public ActionResult GetAll()
        {
            try
            {
                var result = _userAppService.GetAllUsuarioOperador();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("Perfil")]
        public ActionResult GetUserLogado()
        {
            try
            {
                return Ok(GetUsuarioLogado());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut()]
        public ActionResult AtualizarPerfilUsuario([FromBody] UpdatePerfilUsuario request)
        {
            try
            {
                _userAppService.UpdateUserProfile(request, GetUsuarioLogado());
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("RecuperarSenha")]
        public ActionResult RecuperarSenha([FromBody] RecuperarSenhaRequest request)
        {
            try
            {
                _userAppService.RecuperarSenha(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("SolicitarLinkSenha")]
        public ActionResult SolicitarLinkSenha([FromBody] SolicitarLinkSenhaRequest request)
        {
            try
            {
                _userAppService.SolicitarLinkSenha(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

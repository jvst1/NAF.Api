using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using System.Security.Claims;

namespace NAF.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class NafControllerBase : ControllerBase
    {
        private IUserAppService _userAppService;
        public NafControllerBase(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        protected Usuario GetUsuarioLogado()
        {
            var codigoUsuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            Usuario usuario = _userAppService.GetUserByCodigo(Guid.Parse(codigoUsuario));

            if (usuario == null)
                throw new ArgumentNullException("Usuario não configurado para esta funcionalidade.");

            return usuario;
        }
    }
}

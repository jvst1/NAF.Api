using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Enum;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class ServicoController : NafControllerBase
    {
        private readonly IServicoAppService _servicoService;

        public ServicoController(IServicoAppService servicoService, IUserAppService userAppService) : base(userAppService)
        {
            _servicoService = servicoService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult CreateServico(CreateServicoRequest request)
        {
            try
            {
                var response = _servicoService.CreateServico(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllServico()
        {
            try
            {
                var servico = _servicoService.GetAllServico();

                if (servico is null || servico?.Count() == 0)
                    return NoContent();

                return Ok(servico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetServico(Guid id)
        {
            try
            {
                var servico = _servicoService.GetServico(id);
                return Ok(servico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult UpdateServico([FromBody] UpdateServicoRequest request, [FromRoute] Guid id)
        {
            try
            {
                if (!id.Equals(request.Codigo))
                    return BadRequest("O código informado não é o mesmo.");

                _servicoService.UpdateServico(request);
                return Ok("Servico atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult DeleteServico([FromRoute] Guid id)
        {
            try
            {
                _servicoService.DeleteServico(id);
                return Ok("Servico apagado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
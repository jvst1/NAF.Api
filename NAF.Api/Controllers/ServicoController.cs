using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class ServicoController : NafControllerBase
    {
        private readonly IServicoAppService _servicoService;

        public ServicoController(IServicoAppService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpPost]
        public ActionResult CreateServico(CreateServicoRequest request)
        {
            try
            {
                _servicoService.CreateServico(request);

                return Ok("Servico criado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
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
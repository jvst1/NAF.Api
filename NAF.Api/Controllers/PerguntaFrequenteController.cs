using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class PerguntaFrequenteController : NafControllerBase
    {
        private readonly IPerguntaFrequenteAppService _perguntaFrequenteService;

        public PerguntaFrequenteController(IPerguntaFrequenteAppService perguntaFrequenteService)
        {
            _perguntaFrequenteService = perguntaFrequenteService;
        }

        [HttpPost]
        public ActionResult CreatePerguntaFrequente(CreatePerguntaFrequenteRequest request)
        {
            try
            {
                _perguntaFrequenteService.CreatePerguntaFrequente(request);

                return Ok("PerguntaFrequente criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetPerguntaFrequente(Guid id)
        {
            try
            {
                var perguntaFrequente = _perguntaFrequenteService.GetPerguntaFrequente(id);
                return Ok(perguntaFrequente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePerguntaFrequente([FromBody] UpdatePerguntaFrequenteRequest request, [FromRoute] Guid id)
        {
            try
            {
                if (!id.Equals(request.Codigo))
                    return BadRequest("O código informado não é o mesmo.");

                _perguntaFrequenteService.UpdatePerguntaFrequente(request);
                return Ok("PerguntaFrequente atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePerguntaFrequente([FromRoute] Guid id)
        {
            try
            {
                _perguntaFrequenteService.DeletePerguntaFrequente(id);
                return Ok("Pergunta frequente apagada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
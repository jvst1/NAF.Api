using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Enum;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class PerguntaFrequenteController : NafControllerBase
    {
        private readonly IPerguntaFrequenteAppService _perguntaFrequenteService;

        public PerguntaFrequenteController(IPerguntaFrequenteAppService perguntaFrequenteService, IUserAppService userAppService) : base(userAppService)
        {
            _perguntaFrequenteService = perguntaFrequenteService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(TipoPerfil.Professor) + "," + nameof(TipoPerfil.Aluno))]
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
        [AllowAnonymous]
        public ActionResult GetAllPerguntaFrequente()
        {
            try
            {
                var perguntasFrequentes = _perguntaFrequenteService.GetAllPerguntaFrequente();
                if (perguntasFrequentes is null || perguntasFrequentes?.Count == 0)
                    return NoContent();

                return Ok(perguntasFrequentes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
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
        [Authorize(Roles = nameof(TipoPerfil.Professor) + "," + nameof(TipoPerfil.Aluno))]
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
        [Authorize(Roles = nameof(TipoPerfil.Professor) + "," + nameof(TipoPerfil.Aluno))]
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
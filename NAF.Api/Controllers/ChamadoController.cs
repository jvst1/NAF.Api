using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Enum;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class ChamadoController : NafControllerBase
    {
        private readonly IChamadoAppService _chamadoService;

        public ChamadoController(IChamadoAppService chamadoService, IUserAppService userAppService) : base(userAppService)
        {
            _chamadoService = chamadoService;
        }

        [HttpPost]
        public ActionResult CreateChamado(CreateChamadoRequest request)
        {
            try
            {
                var response = _chamadoService.CreateChamado(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAllChamado()
        {
            try
            {
                var chamado = _chamadoService.GetAllChamado(GetUsuarioLogado().Codigo);
                if (chamado == null || chamado?.Count == 0)
                    return NoContent();

                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetChamado(Guid id)
        {
            try
            {
                var chamado = _chamadoService.GetChamado(id);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateChamado([FromBody] UpdateChamadoRequest request, [FromRoute] Guid id)
        {
            try
            {
                if (!id.Equals(request.Codigo))
                    return BadRequest("O código informado não é o mesmo");

                _chamadoService.UpdateChamado(request);
                return Ok("Chamado atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Situacao")]
        [Authorize(Roles = nameof(TipoPerfil.Professor) + "," + nameof(TipoPerfil.Aluno))]
        public ActionResult UpdateChamadoSituacao([FromBody] UpdateChamadoSituacaoRequest request, [FromRoute] Guid id)
        {
            try
            {
                if (!id.Equals(request.Codigo))
                    return BadRequest("O código informado não é o mesmo");

                _chamadoService.UpdateChamadoSituacao(request);
                return Ok("Chamado atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult DeleteChamado([FromRoute] Guid id, Guid codigoUsuario)
        {
            try
            {
                _chamadoService.DeleteChamado(id, codigoUsuario);
                return Ok("Chamado apagado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Documento")]
        public ActionResult CreateChamadoDocumento([FromForm] FileUploadRequest request, [FromRoute] Guid id)
        {
            try
            {
                _chamadoService.CreateChamadoDocumento(request, id);

                return Ok("Upload do documento realizado criada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/Documento")]
        public ActionResult GetAllChamadoDocumento([FromRoute] Guid id)
        {
            try
            {
                var chamado = _chamadoService.GetAllChamadoDocumento(id);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{chamadoId}/Documento/{documentoId}")]
        public ActionResult GetChamadoDocumento([FromRoute] Guid chamadoId, [FromRoute] Guid documentoId)
        {
            try
            {
                var chamado = _chamadoService.GetChamadoDocumento(chamadoId, documentoId);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{chamadoId}/Documento/{documentoId}")]
        public ActionResult DeleteChamadoDocumento([FromRoute] Guid chamadoId, [FromRoute] Guid documentoId, Guid codigoUsuario)
        {
            try
            {
                _chamadoService.DeleteChamadoDocumento(chamadoId, documentoId, codigoUsuario);
                return Ok("O documento foi apagado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Comentario")]
        public ActionResult CreateChamadoComentario([FromForm] CreateChamadoComentarioRequest request, [FromRoute] Guid id)
        {
            try
            {
                _chamadoService.CreateChamadoComentario(request, id);

                return Ok("Comentario adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/Comentario")]
        public ActionResult GetAllChamadoComentario([FromRoute] Guid id)
        {
            try
            {
                var chamado = _chamadoService.GetAllChamadoComentario(id);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{chamadoId}/Comentario/{comentarioId}")]
        public ActionResult UpdateChamadoComentario([FromRoute] Guid chamadoId, [FromRoute] Guid comentarioId, [FromBody] UpdateChamadoComentarioRequest request)
        {
            try
            {
                _chamadoService.UpdateChamadoComentario(chamadoId, comentarioId, request);
                return Ok("O comentario foi atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{chamadoId}/Comentario/{comentarioId}")]
        public ActionResult DeleteChamadoComentario([FromRoute] Guid chamadoId, [FromRoute] Guid comentarioId, Guid codigoUsuario)
        {
            try
            {
                _chamadoService.DeleteChamadoComentario(chamadoId, comentarioId, codigoUsuario);
                return Ok("O comentario foi apagado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/Historico")]
        public ActionResult GetAllChamadoHistorico([FromRoute] Guid id)
        {
            try
            {
                var chamado = _chamadoService.GetAllChamadoHistorico(id);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

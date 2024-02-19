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
                var operador = GetUsuarioLogado();

                _chamadoService.UpdateChamado(request, operador.Codigo, id);
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
                var operador = GetUsuarioLogado();

                _chamadoService.UpdateChamadoSituacao(request, operador.Codigo, id);
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
                var response = _chamadoService.CreateChamadoDocumento(request, id);

                return Ok(response);
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
        public ActionResult DeleteChamadoDocumento([FromRoute] Guid chamadoId, [FromRoute] Guid documentoId)
        {
            try
            {
                var usuario = GetUsuarioLogado();

                _chamadoService.DeleteChamadoDocumento(chamadoId, documentoId, usuario.Codigo);
                return Ok("O documento foi apagado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{chamadoId}/Documento/{documentoId}/Download")]
        public ActionResult DownloadChamadoDocumento([FromRoute] Guid chamadoId, [FromRoute] Guid documentoId)
        {
            try
            {
                var chamadoDocumento = _chamadoService.GetChamadoDocumento(chamadoId, documentoId);

                if (chamadoDocumento == null)
                    throw new KeyNotFoundException("O documento solicitado não foi encontrado na base de dados");

                var filename = Path.GetFileNameWithoutExtension(chamadoDocumento.NomeArquivo);
                var ext = Path.GetExtension(chamadoDocumento.NomeArquivo!).Replace(".", string.Empty);
                var contentType = GetContentType(ext);

                return File(chamadoDocumento.Arquivo!, contentType, chamadoDocumento.NomeArquivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Comentario")]
        public ActionResult CreateChamadoComentario([FromBody] CreateChamadoComentarioRequest request, [FromRoute] Guid id)
        {
            try
            {
                var response = _chamadoService.CreateChamadoComentario(request, id);

                return Ok(response);
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

        [HttpGet("Operador/{id}")]
        public ActionResult GetAllChamadoOperador([FromRoute] Guid id)
        {
            try
            {
                var chamados = _chamadoService.GetAllChamadoOperador(id);
                return Ok(chamados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetContentType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                "txt" => "text/plain",
                "rem" => "text/plain",
                "bmp" => "image/bmp",
                "jpg" => "image/jpeg",
                "jpeg" => "image/jpeg",
                "gif" => "image/gif",
                "png" => "image/png",
                "pdf" => "application/pdf",
                "doc" => "application/msword",
                "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "xls" => "application/msexcel",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "zip" => "application/zip",
                "xml" => "text/xml",
                "json" => "application/json",
                _ => "application/octet-stream",
            };
        }

    }
}

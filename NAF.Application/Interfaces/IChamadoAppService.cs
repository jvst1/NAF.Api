using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IChamadoAppService
    {
        Chamado CreateChamado(CreateChamadoRequest request);
        List<Chamado> GetAllChamado(Guid codigoUsuario);
        Chamado GetChamado(Guid id);
        void UpdateChamado(UpdateChamadoRequest request);
        void UpdateChamadoSituacao(UpdateChamadoSituacaoRequest request);
        void DeleteChamado(Guid id, Guid codigoUsuario);
        ChamadoDocumento CreateChamadoDocumento(FileUploadRequest request, Guid id);
        List<ChamadoDocumento> GetAllChamadoDocumento(Guid chamadoId);
        ChamadoDocumento GetChamadoDocumento(Guid chamadoId, Guid documentoId);
        void DeleteChamadoDocumento(Guid chamadoId, Guid documentoId, Guid codigoUsuario);
        ChamadoComentario CreateChamadoComentario(CreateChamadoComentarioRequest request, Guid id);
        List<ChamadoComentario> GetAllChamadoComentario(Guid chamadoId);
        ChamadoComentario GetChamadoComentario(Guid chamadoId, Guid comentarioId);
        void UpdateChamadoComentario(Guid chamadoId, Guid comentarioId, UpdateChamadoComentarioRequest request);
        void DeleteChamadoComentario(Guid chamadoId, Guid comentarioId, Guid codigoUsuario);
        List<ChamadoHistorico> GetAllChamadoHistorico(Guid id);
        List<dynamic> GetAllChamadoOperador(Guid operadorId);
    }
}

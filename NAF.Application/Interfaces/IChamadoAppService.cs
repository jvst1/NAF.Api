using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IChamadoAppService
    {
        void CreateChamado(CreateChamadoRequest request);
        Chamado GetChamado(Guid id);
        void UpdateChamado(UpdateChamadoRequest request);
        void UpdateChamadoSituacao(UpdateChamadoSituacaoRequest request);
        void DeleteChamado(Guid id);
        void CreateChamadoDocumento(FileUploadRequest request, Guid id);
        List<ChamadoDocumento> GetAllChamadoDocumento(Guid chamadoId);
        ChamadoDocumento GetChamadoDocumento(Guid chamadoId, Guid documentoId);
        void DeleteChamadoDocumento(Guid chamadoId, Guid documentoId);
        void CreateChamadoComentario(CreateChamadoComentarioRequest request, Guid id);
        List<ChamadoComentario> GetAllChamadoComentario(Guid chamadoId);
        ChamadoComentario GetChamadoComentario(Guid chamadoId, Guid comentarioId);
        void UpdateChamadoComentario(Guid chamadoId, Guid comentarioId, UpdateChamadoComentarioRequest request);
        void DeleteChamadoComentario(Guid chamadoId, Guid comentarioId);
    }
}

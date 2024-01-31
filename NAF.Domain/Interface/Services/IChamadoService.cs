using NAF.Domain.Entities;

namespace NAF.Domain.Interface.Services
{
    public interface IChamadoService
    {
        void ValidateChamado(Chamado chamado);
        void ValidateChamadoDocumento(ChamadoDocumento documento);
        void ValidateChamadoComentario(ChamadoComentario comentario);
    }
}

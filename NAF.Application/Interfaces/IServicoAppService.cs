using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IServicoAppService
    {
        Servico CreateServico(CreateServicoRequest request);
        List<Servico> GetAllServico();
        Servico GetServico(Guid id);
        void UpdateServico(UpdateServicoRequest request);
        void DeleteServico(Guid id);
    }
}

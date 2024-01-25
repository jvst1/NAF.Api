using NAF.Domain.Entities;

namespace NAF.Domain.Interface.Repositories
{
    public interface IAreaRepository : IRepositoryBase<Area>
    {
        List<Servico> GetServicosByCodigoArea(Guid codigoArea);
    }
}

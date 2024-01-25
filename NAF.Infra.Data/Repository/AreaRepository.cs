using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;
using System.Data.Entity;

namespace NAF.Infra.Data.Repository
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(DatabaseContext context) : base(context)
        {
        }

        public List<Servico> GetServicosByCodigoArea(Guid codigoArea)
        {
            return base.Db.Set<Servico>()
                            .AsNoTracking()
                            .Where(o => o.CodigoArea.Equals(codigoArea))
                            .ToList();
        }
    }
}

using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class ChamadoHistoricoRepository : RepositoryBase<ChamadoHistorico>, IChamadoHistoricoRepository
    {
        public ChamadoHistoricoRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

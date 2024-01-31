using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class ChamadoDocumentoRepository : RepositoryBase<ChamadoDocumento>, IChamadoDocumentoRepository
    {
        public ChamadoDocumentoRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

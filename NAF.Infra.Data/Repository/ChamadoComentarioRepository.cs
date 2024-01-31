using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class ChamadoComentarioRepository : RepositoryBase<ChamadoComentario>, IChamadoComentarioRepository
    {
        public ChamadoComentarioRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

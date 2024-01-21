using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

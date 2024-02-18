using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class EnvioEmailRepository : RepositoryBase<EnvioEmail>, IEnvioEmailRepository
    {
        public EnvioEmailRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

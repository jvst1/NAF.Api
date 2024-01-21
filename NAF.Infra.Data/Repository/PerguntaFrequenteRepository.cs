using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class PerguntaFrequenteRepository : RepositoryBase<PerguntaFrequente>, IPerguntaFrequenteRepository
    {
        public PerguntaFrequenteRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

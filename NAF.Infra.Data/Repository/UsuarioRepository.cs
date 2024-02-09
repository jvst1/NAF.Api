using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.Context;
using NAF.Infra.Data.Repository.Base;

namespace NAF.Infra.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DatabaseContext context) : base(context)
        {
        }

        public Usuario? GetByEmail(string email)
        {
            return Db.Usuario.FirstOrDefault(o => o.Email!.Equals(email));
        }
        public Usuario? GetByDocumentoFederal(string documentoFederal)
        {
            return Db.Usuario.FirstOrDefault(o => o.DocumentoFederal!.Equals(documentoFederal));
        }
    }
}

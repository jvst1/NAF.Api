using NAF.Domain.Entities;

namespace NAF.Domain.Interface.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario? GetByEmail(string email);
        Usuario? GetByDocumentoFederal(string documentoFederal);
    }
}

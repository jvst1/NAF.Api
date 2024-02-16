using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class ChamadoDocumento : EntityBase
    {
        public Guid CodigoUsuario { get; set; }
        public Guid CodigoChamado { get; set; }
        public string? NomeArquivo { get; set; }
        public byte[]? Arquivo { get; set; }

        public Usuario? Usuario { get; set; }
        public Chamado? Chamado { get; set; }
    }
}

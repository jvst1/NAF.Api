using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class ChamadoDocumento : EntityBase
    {
        public Guid CodigoChamado { get; set; }
        public Guid CodigoUsuario { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Arquivo { get; set; }
    }
}

using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class ChamadoHistorico : EntityBase
    {
        public Guid CodigoChamado { get; set; }
        public Guid CodigoUsuario { get; set; }
        public int TipoAlteracao { get; set; }
        public string CampoAlterado { get; set; }
        public string? ValorAntigo { get; set; }
        public string? ValorNovo { get; set; }
    }
}
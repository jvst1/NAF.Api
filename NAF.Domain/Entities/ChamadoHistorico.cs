using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class ChamadoHistorico : EntityBase
    {
        public int TipoAlteracao { get; set; }
        public string? CampoAlterado { get; set; }
        public string? ValorAntigo { get; set; }
        public string? ValorNovo { get; set; }

        public Guid CodigoUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public Guid CodigoChamado { get; set; }
        public Chamado? Chamado { get; set; }
    }
}
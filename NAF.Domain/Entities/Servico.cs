using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class Servico : EntityBase
    {
        public Guid CodigoArea { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DtAlteracao { get; set; }
        public int HoraComplementar { get; set; }

        public Area? Area { get; set; }
    }
}

using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class Area : EntityBase
    {
        public string? Nome { get; set; }
        public DateTime? DtAlteracao { get; set; }
    }
}

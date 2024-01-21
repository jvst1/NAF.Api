namespace NAF.Domain.Base.Entity
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public Guid Codigo { get; set; }
        public DateTime DtInclusao { get; set; }
    }
}

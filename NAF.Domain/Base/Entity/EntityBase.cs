namespace NAF.Domain.Base.Entity
{
    public abstract class EntityBase
    {
        public Guid Codigo { get; set; }
        public DateTime DtInclusao { get; set; }
    }
}

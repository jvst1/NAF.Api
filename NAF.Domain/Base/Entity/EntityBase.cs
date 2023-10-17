namespace NAF.Domain.Base.Entity
{
    public class EntityBase
    {
        public long Id { get; set; }
        public Guid Codigo { get; set; }
        public DateTime DtInclusao { get; set; }

        public EntityBase()
        {
            DtInclusao = DateTime.Now;
        }
    }
}

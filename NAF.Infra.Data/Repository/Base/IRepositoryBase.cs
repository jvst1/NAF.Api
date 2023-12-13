namespace NAF.Infra.Data.Repository.Base
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity? GetByCodigo(Guid codigo);
        TEntity? GetById(long id);
        int Count();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int SaveChanges();
    }
}

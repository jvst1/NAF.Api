using NAF.Domain.Base.Entity;

namespace NAF.Domain.Base.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
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

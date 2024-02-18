using NAF.Domain.Base.Entity;

namespace NAF.Domain.Interface.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> GetAll();
        TEntity? GetByCodigo(Guid codigo);
        int Count();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int SaveChanges();
    }
}

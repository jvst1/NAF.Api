using NAF.Domain.Base.Entity;
using NAF.Domain.Base.Interface;
using NAF.Infra.Data.Context;
using System.Data.Entity;

namespace NAF.Infra.Data.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected DatabaseContext Db;

        public RepositoryBase(DatabaseContext context)
        {
            Db = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsNoTracking();
        }
        public TEntity? GetByCodigo(Guid codigo)
        {
            return Db.Set<TEntity>()
                     .AsNoTracking()
                     .FirstOrDefault(x => x.Codigo == codigo);
        }
        public TEntity? GetById(long id)
        {
            return Db.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public int Count()
        {
            return Db.Set<TEntity>().Count();
        }
        public void Insert(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
        }
        public void Remove(TEntity entity)
        {
            Db.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            Db.Update(entity);
        }
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
    }
}

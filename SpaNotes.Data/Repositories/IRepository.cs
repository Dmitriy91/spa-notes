using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpaNotes.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, new()
    {
        void Add(params TEntity[] entities);
        void Update(params TEntity[] entities);
        void Delete(params TEntity[] entities);
        void Delete(Expression<Func<TEntity, bool>> condition);
        TEntity GetById(params object[] keyValues);
        TEntity GetSingle(Expression<Func<TEntity, bool>> condition);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition);
        bool Exists(Expression<Func<TEntity, bool>> condition);
        Task CommitAsync();
        void Commit();
    }
}

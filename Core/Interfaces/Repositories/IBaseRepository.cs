using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<List<TEntity>> AddAsync(List<TEntity> entity);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task UpdateAsync(List<TEntity> entities);

        void Update(List<TEntity> entities);
    }
}

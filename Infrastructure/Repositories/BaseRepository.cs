using Core.Entities.Base;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        internal ToDoContext Context { get; }

        bool Disposed { get; set; }

        protected BaseRepository(ToDoContext context)
        {
            this.Context = context;

            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Context.ChangeTracker.AutoDetectChangesEnabled = false;

            Context.ChangeTracker.LazyLoadingEnabled = false;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);

            await Context.SaveChangesAsync();

            Detach(entity);

            return entity;
        }

        public async Task<List<TEntity>> AddAsync(List<TEntity> entity)
        {
            Context.Set<TEntity>().AddRange(entity);

            await Context.SaveChangesAsync();

            this.DetachArray(entity);

            return entity;
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().AsNoTracking().Where(predicate);
        }

        public async Task UpdateAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Context.Entry(entity).State = EntityState.Modified;

            await this.Context.SaveChangesAsync();

            foreach (var entity in entities)
                this.Detach(entity);
        }

        public void Update(List<TEntity> entities)
        {
            foreach(var entity in entities)
                this.Context.Entry(entity).State = EntityState.Modified;

            this.Context.SaveChanges();

            foreach (var entity in entities)
                this.Detach(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;

            await this.Context.SaveChangesAsync();

            this.Detach(entity);
        }

        public void Update(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;

            this.Context.SaveChanges();

            this.Detach(entity);
        }

        public void DetachArray(IEnumerable<object> objects)
        {
            objects.ToList().ForEach(obj => Detach(obj));
        }

        public void Detach(object obj)
        {
            Context.Entry(obj).State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.DisposeObject(true);
            GC.SuppressFinalize(this);
        }

        protected void DisposeObject(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }

                this.Disposed = true;
            }
        }
    }
}

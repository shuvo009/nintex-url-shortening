using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Repository.DbContext;

namespace Nintex.Url.Shortening.Repository.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal readonly ShortUrlDbContext ShortUrlDbContext;
        protected DbSet<TEntity> InternalSet;

        protected GenericRepository(ShortUrlDbContext shortUrlDbContext)
        {
            ShortUrlDbContext = shortUrlDbContext;
            InternalSet = ShortUrlDbContext.Set<TEntity>();
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> pExpression)
        {
            return InternalSet.Where(pExpression).ToListAsync();
        }

        public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> pExpression)
        {
            return InternalSet.FirstOrDefaultAsync(pExpression);
        }

        public Task Remove(TEntity model)
        {
            ShortUrlDbContext.Entry(model).State = EntityState.Deleted;
            return Save();
        }

        public Task Insert(TEntity model)
        {
            ShortUrlDbContext.Entry(model).State = EntityState.Added;
            return Save();
        }

        #region Supported Methods

        internal Task Save()
        {
            return ShortUrlDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
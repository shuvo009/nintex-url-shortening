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
        internal readonly ShortUrlDbContext WebProDbContext;
        protected DbSet<TEntity> InternalSet;

        protected GenericRepository(ShortUrlDbContext webProDbContext)
        {
            WebProDbContext = webProDbContext;
            InternalSet = WebProDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return InternalSet;
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> pExpression)
        {
            return InternalSet.Where(pExpression).ToListAsync();
        }

        public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> pExpression)
        {
            return InternalSet.FirstOrDefaultAsync(pExpression);
        }

        public Task Save()
        {
            return WebProDbContext.SaveChangesAsync();
        }

        public Task Insert(TEntity model)
        {
            WebProDbContext.Entry(model).State = EntityState.Added;
            return Save();
        }

        public Task Update(TEntity model)
        {
            WebProDbContext.Entry(model).State = EntityState.Modified;
            return Save();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nintex.Url.Shortening.Core.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class 
    {
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> pExpression);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> pExpression);
        Task Remove(TEntity model);
        Task Insert(TEntity model);
    }
}

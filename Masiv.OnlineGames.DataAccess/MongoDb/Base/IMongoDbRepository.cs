using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;

namespace Masiv.OnlineGames.DataAccess.MongoDb.Base
{
    public interface IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(string id);
        IList<TEntity> SearchForAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
    }
}

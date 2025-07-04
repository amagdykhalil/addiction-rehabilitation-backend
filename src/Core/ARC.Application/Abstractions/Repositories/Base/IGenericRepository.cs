using ARC.Domain.Interfaces;
using System.Linq.Expressions;

namespace ARC.Application.Contracts.Persistence.Base
{
    /// <summary>
    /// Generic repository interface that defines common database operations for entities.
    /// </summary>
    /// <typeparam name="Entity">The type of entity this repository handles.</typeparam>
    public interface IGenericRepository<Entity> where Entity : IEntity
    {
        Task<Entity?> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsNoTracking();
        IQueryable<Entity> GetAllAsTracking();
        Task AddRangeAsync(ICollection<Entity> entities);
        Task AddAsync(Entity entity);
        Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression);
        Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate);
        void Delete(Entity entity);
        Task DeleteAsync(int id);
        Task<bool> isExistsById(int id);

    }
}




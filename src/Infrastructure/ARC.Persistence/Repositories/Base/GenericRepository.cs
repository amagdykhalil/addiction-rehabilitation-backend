
using ARC.Application.Contracts.Persistence.Base;
using ARC.Domain.Interfaces;
using System.Linq.Expressions;

namespace ARC.Persistence.Repositories.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class, IEntity
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Entity> Entities { get; set; }

        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            Entities = _dbContext.Set<Entity>();
        }

        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<List<Entity>> GetAllAsNoTracking()
        {
            return await Entities.AsNoTracking().ToListAsync();
        }

        public IQueryable<Entity> GetAllAsTracking()
        {
            return Entities.AsQueryable();
        }

        public async Task AddRangeAsync(ICollection<Entity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public async Task AddAsync(Entity entity)
        {
            await Entities.AddAsync(entity);
        }

        public async Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression)
        {
            await Entities.ExecuteUpdateAsync(x => x.SetProperty(propertyExpression, valueExpression));
        }

        public async Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate)
        {
            await Entities.Where(predicate).ExecuteDeleteAsync();
        }

        public void Delete(Entity entity)
        {
            Entities.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Entities.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> isExistsById(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}




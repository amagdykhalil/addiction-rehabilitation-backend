using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
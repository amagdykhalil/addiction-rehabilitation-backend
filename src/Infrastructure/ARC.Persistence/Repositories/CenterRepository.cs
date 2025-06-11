using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class CenterRepository : GenericRepository<Center>, ICenterRepository
    {
        private readonly AppDbContext _context;
        public CenterRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
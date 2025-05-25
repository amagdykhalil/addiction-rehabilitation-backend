using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class PlacementRepository : GenericRepository<Placement>, IPlacementRepository
    {
        private readonly AppDbContext _context;
        public PlacementRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
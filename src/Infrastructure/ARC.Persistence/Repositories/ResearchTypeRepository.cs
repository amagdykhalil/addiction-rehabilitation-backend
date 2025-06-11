using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResearchTypeRepository : GenericRepository<ResearchType>, IResearchTypeRepository
    {
        private readonly AppDbContext _context;
        public ResearchTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResidenceInfoRepository : GenericRepository<ResidenceInfo>, IResidenceInfoRepository
    {
        private readonly AppDbContext _context;
        public ResidenceInfoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
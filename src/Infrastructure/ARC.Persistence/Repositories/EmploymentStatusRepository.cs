using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class EmploymentStatusRepository : GenericRepository<EmploymentStatus>, IEmploymentStatusRepository
    {
        private readonly AppDbContext _context;
        public EmploymentStatusRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
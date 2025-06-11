using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class EmergencyContactRepository : GenericRepository<EmergencyContact>, IEmergencyContactRepository
    {
        private readonly AppDbContext _context;
        public EmergencyContactRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
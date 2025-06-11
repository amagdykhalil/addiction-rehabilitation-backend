using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class EmergencyContactAddressRepository : GenericRepository<EmergencyContactAddress>, IEmergencyContactAddressRepository
    {
        private readonly AppDbContext _context;
        public EmergencyContactAddressRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
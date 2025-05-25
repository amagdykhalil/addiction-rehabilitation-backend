using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
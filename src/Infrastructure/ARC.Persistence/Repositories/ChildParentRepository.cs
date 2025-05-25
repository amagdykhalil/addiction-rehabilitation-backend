using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ChildParentRepository : GenericRepository<ChildParent>, IChildParentRepository
    {
        private readonly AppDbContext _context;
        public ChildParentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
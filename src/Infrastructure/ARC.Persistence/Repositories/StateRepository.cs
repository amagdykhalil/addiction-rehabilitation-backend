using ARC.Persistence.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        private readonly AppDbContext _context;
        public StateRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
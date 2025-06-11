using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResearchQuestionRepository : GenericRepository<ResearchQuestion>, IResearchQuestionRepository
    {
        private readonly AppDbContext _context;
        public ResearchQuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
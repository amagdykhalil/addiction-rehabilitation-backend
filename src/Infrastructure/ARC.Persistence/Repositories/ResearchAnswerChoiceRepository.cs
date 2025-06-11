using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResearchAnswerChoiceRepository : GenericRepository<ResearchAnswerChoice>, IResearchAnswerChoiceRepository
    {
        private readonly AppDbContext _context;
        public ResearchAnswerChoiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
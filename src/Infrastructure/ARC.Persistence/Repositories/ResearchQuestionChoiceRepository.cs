using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResearchQuestionChoiceRepository : GenericRepository<ResearchQuestionChoice>, IResearchQuestionChoiceRepository
    {
        private readonly AppDbContext _context;
        public ResearchQuestionChoiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
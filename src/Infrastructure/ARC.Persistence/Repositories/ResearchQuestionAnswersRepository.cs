using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class ResearchQuestionAnswersRepository : GenericRepository<ResearchQuestionAnswer>, IResearchQuestionAnswerRepository
    {
        private readonly AppDbContext _context;
        public ResearchQuestionAnswersRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

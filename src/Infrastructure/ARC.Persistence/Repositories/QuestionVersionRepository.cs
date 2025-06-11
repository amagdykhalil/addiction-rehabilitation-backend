using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class QuestionVersionRepository : GenericRepository<QuestionVersion>, IQuestionVersionRepository
    {
        private readonly AppDbContext _context;
        public QuestionVersionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
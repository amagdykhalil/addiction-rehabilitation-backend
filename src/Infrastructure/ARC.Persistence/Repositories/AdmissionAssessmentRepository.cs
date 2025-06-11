using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Persistence.Repositories
{
    public class AdmissionAssessmentRepository : GenericRepository<AdmissionAssessment>, IAdmissionAssessmentRepository
    {
        private readonly AppDbContext _context;
        public AdmissionAssessmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
} 
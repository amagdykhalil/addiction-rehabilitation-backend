using ARC.Application.Abstractions.Repositories;
using ARC.Application.Common.Models;
using ARC.Application.Features.Patients.Queries.GetPatients;
using ARC.Domain.Enums;

namespace ARC.Persistence.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            return await _context.Patients
                .Include(p => p.Person)
                .ThenInclude(p => p.Nationality)
                .FirstOrDefaultAsync(p => p.Person.NationalIdNumber == nationalId, cancellationToken);
        }

        public async Task<Patient?> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Patients
                .Include(p => p.Person)
                .ThenInclude(p => p.Nationality)
                .FirstOrDefaultAsync(p => p.Person.PassportNumber == passportNumber, cancellationToken);
        }

        public async Task<Patient?> GetByIdWithPersonAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Patients
                .Include(p => p.Person)
                .ThenInclude(p => p.Nationality)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }


        public async Task<PagedResult<Patient>> GetPatientsAsync(string? searchQuery, enGender? gender, int? nationalityId, PatientSortBy? sortBy, SortDirection? sortDirection, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Patients
                .Include(p => p.Person)
                .ThenInclude(p => p.Nationality)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(p =>
                    (p.Person.FirstName + " " + p.Person.SecondName + " " + p.Person.ThirdName + " " + p.Person.LastName).Contains(searchQuery) ||
                    p.Person.CallPhoneNumber.Contains(searchQuery) ||
                    p.Person.NationalIdNumber.Contains(searchQuery) ||
                    p.Person.PassportNumber.Contains(searchQuery)
                );
            }
            if (gender != null)
            {
                query = query.Where(p => p.Person.Gender == gender);
            }
            if (nationalityId != null)
            {

                query = query.Where(p => p.Person.NationalityId == nationalityId);
            }

            // Sorting
            switch (sortBy)
            {
                case PatientSortBy.FirstName:
                    query = sortDirection == SortDirection.Desc ? query.OrderByDescending(p => p.Person.FirstName) : query.OrderBy(p => p.Person.FirstName);
                    break;
                case PatientSortBy.LastName:
                    query = sortDirection == SortDirection.Desc ? query.OrderByDescending(p => p.Person.LastName) : query.OrderBy(p => p.Person.LastName);
                    break;
                case PatientSortBy.NationalId:
                    if (sortDirection == SortDirection.Desc)
                        query = query.OrderByDescending(p => p.Person.NationalIdNumber ?? p.Person.PassportNumber);
                    else
                        query = query.OrderBy(p => p.Person.NationalIdNumber ?? p.Person.PassportNumber);
                    break;
                default:
                    query = sortDirection == SortDirection.Desc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
                    break;
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Patient>(data, pageNumber, pageSize, totalCount);
        }

        public async Task<int?> isExistsByNationalIdNumber(string NationalIdNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Patients.Where(e => e.Person.NationalIdNumber == NationalIdNumber)
                .Select(e => (int?)e.Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> isExistsByPasswordAsync(string Password, CancellationToken cancellationToken = default)
        {
            return await _context.Patients.Where(e => e.Person.PassportNumber == Password)
                .Select(e => (int?)e.Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Patient?> GetByIdInlcudePersonAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Patients.Include(e => e.Person).Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
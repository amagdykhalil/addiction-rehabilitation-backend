using ARC.Application.Features.Patients.Queries.GetPatients;
using ARC.Domain.Enums;

namespace ARC.Application.Abstractions.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>, IRepository
    {
        Task<Patient?> GetByNationalIdAsync(string nationalId);
        Task<Patient?> GetByPassportNumberAsync(string passportNumber);
        Task<Patient?> GetByIdWithPersonAsync(int id);
        Task<int?> isExistsByNationalIdNumber(string NationalIdNumber);
        Task<int?> isExistsByPassword(string Password);
        Task<Common.Models.PagedResult<Patient>> GetPatientsAsync(string? searchQuery, enGender? gender, int? nationalityId, PatientSortBy? sortBy, Common.Models.SortDirection? sortDirection, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
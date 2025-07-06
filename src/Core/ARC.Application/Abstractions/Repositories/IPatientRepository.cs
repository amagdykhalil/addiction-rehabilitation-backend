using ARC.Application.Features.Patients.Queries.GetPatients;
using ARC.Domain.Enums;

namespace ARC.Application.Abstractions.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>, IRepository
    {
        Task<Patient?> GetByIdInlcudePersonAsync(int id, CancellationToken cancellationToken = default);
        Task<Patient?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
        Task<Patient?> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken = default);
        Task<Patient?> GetByIdWithPersonAsync(int id, CancellationToken cancellationToken = default);
        Task<int?> isExistsByNationalIdNumber(string NationalIdNumber, CancellationToken cancellationToken = default);
        Task<int?> isExistsByPasswordAsync(string Password, CancellationToken cancellationToken = default);
        Task<Common.Models.PagedResult<Patient>> GetPatientsAsync(string? searchQuery, enGender? gender, int? nationalityId, PatientSortBy? sortBy, Common.Models.SortDirection? sortDirection, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
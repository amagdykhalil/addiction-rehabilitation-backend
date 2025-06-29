using ARC.Application.Common.Models;
using ARC.Application.Features.Patients.Queries.Models;
using ARC.Domain.Enums;

namespace ARC.Application.Features.Patients.Queries.GetPatients
{
    public enum PatientSortBy
    {
        Id,
        FirstName,
        LastName,
        NationalId
    }

    public record GetPatientsQuery : PaginatedQueryBase<PatientDetailsDto>
    {
        // Filtering
        public enGender? Gender { get; set; }
        public int? NationalityId { get; set; }

        // Sorting
        public PatientSortBy? SortBy { get; set; }

    }
}
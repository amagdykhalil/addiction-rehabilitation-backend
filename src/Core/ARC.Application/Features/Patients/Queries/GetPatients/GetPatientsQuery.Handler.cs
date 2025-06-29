using ARC.Application.Abstractions.Repositories;
using ARC.Application.Common.Queries;
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler : IPaginatedQueryHandler<GetPatientsQuery, PatientDetailsDto>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<Common.Models.PagedResult<PatientDetailsDto>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var pagedPatients = await _patientRepository.GetPatientsAsync(
                request.SearchQuery,
                request.Gender,
                request.NationalityId,
                request.SortBy,
                request.SortDirection,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var mapped = pagedPatients.Data.Select(PatientDetailsDtoMappingExtensions.ToDto).ToList();
            var result = new Common.Models.PagedResult<PatientDetailsDto>(mapped, pagedPatients.CurrentPage, pagedPatients.PageSize, pagedPatients.TotalCount);
            return Result.Success(result);
        }
    }
}
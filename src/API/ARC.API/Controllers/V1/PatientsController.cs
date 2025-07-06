using ARC.Application.Features.Patients.Commands.Create;
using ARC.Application.Features.Patients.Commands.Delete;
using ARC.Application.Features.Patients.Commands.Update;
using ARC.Application.Features.Patients.Queries.ExistsById;
using ARC.Application.Features.Patients.Queries.ExistsByNationalId;
using ARC.Application.Features.Patients.Queries.ExistsByPassport;
using ARC.Application.Features.Patients.Queries.GetById;
using ARC.Application.Features.Patients.Queries.GetByNationalId;
using ARC.Application.Features.Patients.Queries.GetByPassport;
using ARC.Application.Features.Patients.Queries.GetPatients;
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/patients")]

    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new patient with associated person details.
        /// </summary>
        /// <param name="command">The patient creation details.</param>
        /// <returns>Returns the ID of the newly created patient.</returns>
        [HttpPost]
        [ApiResponse(StatusCodes.Status201Created, typeof(int))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Creates a new patient with associated person details.")]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates an existing patient and their person details.
        /// </summary>
        /// <param name="command">The patient update details.</param>
        /// <returns>Returns true if the update was successful.</returns>
        [HttpPut()]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Updates an existing patient and their person details.")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Deletes a patient by ID.
        /// </summary>
        /// <param name="id">The patient ID.</param>
        /// <returns>Returns true if the deletion was successful.</returns>
        [HttpDelete("{Id}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Deletes a patient by ID.")]
        public async Task<IActionResult> Delete([FromRoute] DeletePatientCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a patient by their ID.
        /// </summary>
        /// <param name="id">The patient ID.</param>
        /// <returns>Returns the patient details if found.</returns>
        [HttpGet("{Id}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(PatientDetailsDto))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a patient by their ID.")]
        public async Task<IActionResult> GetById([FromRoute] GetPatientByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a patient by their national ID number.
        /// </summary>
        /// <param name="nationalId">The national ID number.</param>
        /// <returns>Returns the patient details if found.</returns>
        [HttpGet("by-national-id/{NationalId}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(PatientDetailsDto))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a patient by their national ID number.")]
        public async Task<IActionResult> GetByNationalId([FromRoute] GetPatientByNationalIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a patient by their passport number.
        /// </summary>
        /// <param name="passportNumber">The passport number.</param>
        /// <returns>Returns the patient details if found.</returns>
        [HttpGet("by-passport/{PassportNumber}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(PatientDetailsDto))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a patient by their passport number.")]
        public async Task<IActionResult> GetByPassportNumber([FromRoute] GetPatientByPassportQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a paginated, filtered, and sorted list of patients.
        /// </summary>
        /// <returns>Paged result of patients.</returns>
        [HttpGet]
        [ApiResponse(StatusCodes.Status200OK, typeof(ARC.Application.Common.Models.PagedResult<PatientDetailsDto>))]
        [EndpointDescription("Gets a paginated, filtered, and sorted list of patients.")]
        public async Task<IActionResult> GetPatients(
            [FromQuery] GetPatientsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a patient exists by passport number.
        /// </summary>
        /// <param name="query">The query with passport number.</param>
        /// <returns>Returns the patient ID if found, null otherwise.</returns>
        [HttpGet("exists-by-passport/{PassportNumber}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(int?))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a patient exists by passport number.")]
        public async Task<IActionResult> ExistsByPassport([FromRoute] PatientExistsByPassportQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a patient exists by national ID number.
        /// </summary>
        /// <param name="query">The query with national ID.</param>
        /// <returns>Returns the patient ID if found, null otherwise.</returns>
        [HttpGet("exists-by-national-id/{NationalId}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(int?))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a patient exists by national ID number.")]
        public async Task<IActionResult> ExistsByNationalId([FromRoute] PatientExistsByNationalIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a patient exists by ID.
        /// </summary>
        /// <param name="query">The query with patient ID.</param>
        /// <returns>Returns true if patient exists, false otherwise.</returns>
        [HttpGet("exists-by-id/{Id}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a patient exists by ID.")]
        public async Task<IActionResult> ExistsById([FromRoute] PatientExistsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }
    }
}
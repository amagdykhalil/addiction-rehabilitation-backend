using ARC.Application.Features.Users.Commands.Create;
using ARC.Application.Features.Users.Commands.Delete;
using ARC.Application.Features.Users.Commands.Reactivate;
using ARC.Application.Features.Users.Commands.Update;
using ARC.Application.Features.Users.Commands.UpdateUserRoles;
using ARC.Application.Features.Users.Queries.ExistsById;
using ARC.Application.Features.Users.Queries.ExistsByNationalId;
using ARC.Application.Features.Users.Queries.ExistsByPassport;
using ARC.Application.Features.Users.Queries.GetAllRoles;
using ARC.Application.Features.Users.Queries.GetByEmail;
using ARC.Application.Features.Users.Queries.GetById;
using ARC.Application.Features.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace ARC.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<UsersController> _localizer;

        public UsersController(IMediator mediator, IStringLocalizer<UsersController> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        /// <summary>
        /// Creates a new user with person data.
        /// </summary>
        /// <param name="command">The user creation command.</param>
        /// <returns>The ID of the created user.</returns>
        [HttpPost]
        [ApiResponse(StatusCodes.Status201Created, typeof(int))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Creates a new user with person data.")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="command">The user update command.</param>
        /// <returns>Success status.</returns>
        [HttpPut]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Updates an existing user.")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Deactivates (soft deletes) a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>Success status.</returns>
        [HttpPost("deactivate/{Id}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Deactivates (soft deletes) a user.")]
        public async Task<IActionResult> Deactivate([FromRoute] DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Update roles from a user.
        /// </summary>
        [HttpPost("update-roles")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Update roles from a user.")]
        public async Task<IActionResult> RemoveRoles([FromBody] UpdateUserRolesCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Reactivates a user (undoes soft delete).
        /// </summary>
        [HttpPost("reactivate/{Id}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Reactivates a user (undoes soft delete).")]
        public async Task<IActionResult> Reactivate([FromRoute] ReactivateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        [HttpGet("{Id}")]
        [ApiResponse(StatusCodes.Status200OK)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a user by their ID.")]
        public async Task<IActionResult> GetById([FromRoute] GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a paginated, filtered, and sorted list of users.
        /// </summary>
        /// <returns>Paged result of users.</returns>
        [HttpGet]
        [ApiResponse(StatusCodes.Status200OK, typeof(ARC.Application.Common.Models.PagedResult<UserDetailsDto>))]
        [EndpointDescription("Gets a paginated, filtered, and sorted list of users.")]
        public async Task<IActionResult> GetUsers(
            [FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a user exists by ID.
        /// </summary>
        [HttpGet("exists-by-id/{Id}")]
        [ApiResponse(StatusCodes.Status200OK)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a user exists by ID.")]
        public async Task<IActionResult> ExistsById([FromRoute] UserExistsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a user exists by national ID number.
        /// </summary>
        [HttpGet("exists-by-national-id/{NationalIdNumber}")]
        [ApiResponse(StatusCodes.Status200OK)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a user exists by national ID number.")]
        public async Task<IActionResult> ExistsByNationalId([FromRoute] UserExistsByNationalIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Checks if a user exists by passport number.
        /// </summary>
        [HttpGet("exists-by-passport/{PassportNumber}")]
        [ApiResponse(StatusCodes.Status200OK)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Checks if a user exists by passport number.")]
        public async Task<IActionResult> ExistsByPassport([FromRoute] UserExistsByPassportQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        [HttpGet("exists-by-email/{Email}")]
        [ApiResponse(StatusCodes.Status200OK)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a user by their email.")]
        public async Task<IActionResult> GetByEmail([FromRoute] UserExitsByEmailQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        [HttpGet("{UserId}/roles")]
        [ApiResponse(StatusCodes.Status200OK)]
        [EndpointDescription("Gets all user roles.")]
        public async Task<IActionResult> GetAllRoles([FromRoute] GetAllUserRolesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }
    }
}
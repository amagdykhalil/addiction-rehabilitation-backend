using ARC.Application.Features.Roles.Commands.AddRoles;
using ARC.Application.Features.Roles.Commands.UpdateRole;
using ARC.Application.Features.Roles.Models;
using ARC.Application.Features.Roles.Queries.GetRoleById;
using ARC.Application.Features.Users.Commands.RemoveRoles;
using ARC.Application.Features.Users.Queries.GetAllRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace ARC.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/roles")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<RolesController> _localizer;

        public RolesController(IMediator mediator, IStringLocalizer<RolesController> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        /// <summary>
        /// Adds new roles.
        /// </summary>
        [HttpPost]
        [ApiResponse(StatusCodes.Status201Created, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Adds new roles.")]
        public async Task<IActionResult> AddRoles([FromBody] AddRolesCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Removes roles by IDs.
        /// </summary>
        [HttpPost("remove")]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Removes roles by IDs.")]
        public async Task<IActionResult> RemoveRoles([FromBody] RemoveRolesCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates a role.
        /// </summary>
        [HttpPut]
        [ApiResponse(StatusCodes.Status200OK, typeof(bool))]
        [ApiResponse(StatusCodes.Status400BadRequest)]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Updates a role.")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult();
        }

        // <summary>
        /// Gets a role by its ID.
        /// </summary>
        [HttpGet("{Id:int}")]
        [ApiResponse(StatusCodes.Status200OK, typeof(RoleDto))]
        [ApiResponse(StatusCodes.Status404NotFound)]
        [EndpointDescription("Gets a role by its ID.")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        [HttpGet]
        [ApiResponse(StatusCodes.Status200OK, typeof(List<RoleDto>))]
        [EndpointDescription("Gets all roles.")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return result.ToActionResult();
        }
    }
}

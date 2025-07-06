using ARC.Application.Features.Users.Queries.Models;
using Application.Abstractions.Messaging;
using System.Collections.Generic;
using ARC.Application.Features.Roles.Models;

namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IQuery<List<RoleDto>>;
} 
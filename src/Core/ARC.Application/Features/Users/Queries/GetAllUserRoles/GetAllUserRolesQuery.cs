using ARC.Application.Features.Users.Queries.Models;
using Application.Abstractions.Messaging;
using System.Collections.Generic;

namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public record GetAllUserRolesQuery : IQuery<List<GetAllUserRolesQueryResponse>>
    {
        public int UserId { get; set; }
    }
} 
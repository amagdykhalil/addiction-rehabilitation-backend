using ARC.Application.Common.Models;
using ARC.Application.Features.Users.Queries.GetById;
using ARC.Domain.Enums;

namespace ARC.Application.Features.Users.Queries.GetUsers
{
    public enum UserSortBy
    {
        Id,
        FirstName,
        LastName,
        NationalId
    }
    public record GetUsersQuery : PaginatedQueryBase<UserDetailsDto>
    {
        // Filtering
        public enGender? Gender { get; set; }
        public int? NationalityId { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }

        // Sorting
        public UserSortBy? SortBy { get; set; }

    }
}

using ARC.Application.Abstractions.UserContext;
using ARC.Application.Common.Models;
using ARC.Application.Features.Roles.Models;
using ARC.Application.Features.Users.Queries.GetUsers;
using ARC.Application.Features.Users.Queries.Models;
using ARC.Domain.Enums;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace ARC.Persistence.Identity
{
    /// <summary>
    /// Service for managing user identity operations using ASP.NET Core Identity.
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;

        public IdentityService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<User?> GetUserAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, password);
            return result ? user : null;
        }

        public async Task<IList<string?>> GetRolesAsync(int userId, string lang = "en", CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new List<string?>();

            var userRoleNames = await _userManager.GetRolesAsync(user); // These are the role names (Name)

            return await _roleManager.Roles
                .Where(r => userRoleNames.Contains(r.Name))
                .Select(r => lang == "ar" ? r.Name_ar : r.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task AddUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return;
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return;
            await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<IdentityResult> ValidatePasswordAsync(string password, CancellationToken cancellationToken = default)
        {
            // We pass null as the user because we only care about the rules,
            // not whether it matches an existing user's password.
            return await _userManager.PasswordValidators[0]
                          .ValidateAsync(_userManager, null!, password);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.CreateAsync(user);
        }

        // User management methods
        public async Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<User?> GetUserByIdIncludePersonAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users
                .Include(u => u.Person)
                .ThenInclude(p => p.Nationality)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return user; // Return the user or null if not found
        }

        public async Task<IdentityResult> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        // Email confirmation methods
        public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string code, CancellationToken cancellationToken = default)
        {
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string code, CancellationToken cancellationToken = default)
        {
            return await _userManager.ChangeEmailAsync(user, newEmail, code);
        }

        public async Task<IdentityResult> SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
        {
            return await _userManager.SetUserNameAsync(user, userName);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail, CancellationToken cancellationToken = default)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        // Password reset methods
        public async Task<string> GeneratePasswordResetTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string newPassword, CancellationToken cancellationToken = default)
        {

            return await _userManager.ResetPasswordAsync(user, code, newPassword);
        }

        public async Task AddUserRolesAsync(int userId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return;
            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.Name).ToList();
            await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task RemoveFromRolesAsync(IEnumerable<int> roleIds, CancellationToken cancellationToken = default)
        {
            var roles = await _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync(cancellationToken);
            if (roles.Any())
            {
                _context.Set<Role>().RemoveRange(roles);
            }
        }

        public async Task<User?> FindByEmailIncludePersonAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users.Include(e => e.Person).Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> IsExistsByPasswordAsync(string PassportNumber, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users
                .Where(u => u.Person.PassportNumber == PassportNumber)
                .Select(u => (int?)u.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> IsExistsByNationalIdNumber(string NationalIdNumber, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users
                .Where(u => u.Person.NationalIdNumber == NationalIdNumber)
                .Select(u => (int?)u.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> IsExitsByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users
               .Where(u => u.Email == email)
               .Select(u => (int?)u.Id)
               .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<UserWithRolesDto>> GetUsersAsync(string? searchQuery, enGender? gender, int? nationalityId, int? roleId, bool? isActive, UserSortBy? sortBy, SortDirection? sortDirection, int pageNumber, int pageSize, string lang, CancellationToken cancellationToken)
        {
            // Get connection string from DbContext
            var connectionString = _context.Database.GetConnectionString();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            // Map enum values to stored procedure parameters
            var sortByParam = sortBy?.ToString() ?? "Id";
            var sortDirectionParam = sortDirection?.ToString() ?? "ASC";
            var genderParam = gender.HasValue ? (int)gender.Value : (int?)null;
            var isActiveParam = isActive;

            // Call stored procedure
            var parameters = new
            {
                SearchQuery = searchQuery,
                Gender = genderParam,
                NationalityId = nationalityId,
                RoleId = roleId,
                IsActive = isActiveParam,
                SortBy = sortByParam,
                SortDirection = sortDirectionParam,
                PageNumber = pageNumber,
                PageSize = pageSize,
                locale = lang,
            };

            var command = new CommandDefinition(
                    commandText: "GetUsers",
                    parameters: parameters,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: 30,
                    cancellationToken: cancellationToken
                );

            var results = await connection.QueryAsync<UserWithRolesDto>(command);

            var usersWithRoles = results.ToList();

            if (!usersWithRoles.Any())
            {
                return new PagedResult<UserWithRolesDto>(new List<UserWithRolesDto>(), pageNumber, pageSize, 0);
            }

            // Get total count from first result
            var totalCount = usersWithRoles.First().TotalCount;

            return new PagedResult<UserWithRolesDto>(usersWithRoles, pageNumber, pageSize, totalCount);
        }

        public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _roleManager.Roles.ToListAsync(cancellationToken);
        }

        public async Task UpdateUserRolesAsync(int userId, IEnumerable<int> roleIds, CancellationToken cancellationToken = default)
        {
            // Get connection string from DbContext
            var connectionString = _context.Database.GetConnectionString();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            var idsTable = ToIdListDataTable(roleIds);

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            parameters.Add("Ids", idsTable.AsTableValuedParameter("IdList"));

            await connection.ExecuteAsync(
                "UpdateUserRoles",
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        private static System.Data.DataTable ToIdListDataTable(IEnumerable<int> ids)
        {
            var table = new System.Data.DataTable();
            table.Columns.Add("Id", typeof(int));
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    table.Rows.Add(id);
                }
            }
            return table;
        }

        public async Task<List<Role>> GetAllUserRolesAsync(int userId, CancellationToken cancellationToken = default)
        {
            // Get all role IDs for the user from the UserRoles join table
            var roleIds = await _context.Set<IdentityUserRole<int>>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync(cancellationToken);

            // Get the Role entities for those IDs
            var roles = await _roleManager.Roles
                .Where(r => roleIds.Contains(r.Id))
                .ToListAsync(cancellationToken);

            return roles;
        }
        public async Task AddRolesAsync(IEnumerable<RoleDto> roles, CancellationToken cancellationToken = default)
        {
            var rolesList = roles.Select(r =>
            new Role
            {
                Name = r.Name_en,
                Name_ar = r.Name_ar,
                NormalizedName = r.Name_en.ToUpperInvariant()
            })
            .ToList();

            await _context.Set<Role>()
                .AddRangeAsync(rolesList, cancellationToken);
        }
        public async Task<Role?> GetRoleAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _roleManager.Roles
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
    }
}




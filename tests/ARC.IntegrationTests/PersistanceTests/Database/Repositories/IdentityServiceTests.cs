using ARC.Application.Features.Roles.Models;
using ARC.Application.Features.Users.Queries.GetUsers;
using ARC.Domain.Entities;
using ARC.IntegrationTests.PersistanceTests.Database.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ARC.IntegrationTests.PersistanceTests.Database.Repositories
{
    public class IdentityServiceTests : BaseDatabaseTests
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public IdentityServiceTests(DatabaseTestEnvironmentFixture fixture) : base(fixture)
        {
            _identityService = ServiceProvider.GetRequiredService<IIdentityService>();
            _userManager = ServiceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = ServiceProvider.GetRequiredService<RoleManager<Role>>();
        }

        [Fact]
        public async Task GetUsersAsync_WithValidParameters_ReturnsPagedUsers()
        {
            // Arrange
            var users = TestDataGenerators.UserFaker().Generate(5);
            await DbContext.Set<User>().AddRangeAsync(users);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await _identityService.GetUsersAsync(
                searchQuery: null,
                gender: null,
                nationalityId: null,
                roleId: null,
                IsActive: null,
                sortBy: UserSortBy.Id,
                sortDirection: Application.Common.Models.SortDirection.Asc,
                pageNumber: 1,
                pageSize: 10,
                lang: "en",
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count == 5);
            Assert.True(result.TotalCount == 5);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.PageSize);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("ar")]
        public async Task GetUsersAsync_WithLanguageParameter_ReturnsLocalizedResults(string language)
        {
            // Arrange
            var users = TestDataGenerators.UserFaker().Generate(5);
            await DbContext.Set<User>().AddRangeAsync(users);
            await UnitOfWork.SaveChangesAsync();

            var role = TestDataGenerators.RoleFaker().Generate();
            await _roleManager.CreateAsync(role);
            await _userManager.AddToRoleAsync(users[0], role.Name);


            // Act
            var result = await _identityService.GetUsersAsync(
                searchQuery: null,
                gender: null,
                nationalityId: null,
                roleId: null,
                IsActive: null,
                sortBy: UserSortBy.Id,
                sortDirection: Application.Common.Models.SortDirection.Asc,
                pageNumber: 1,
                pageSize: 10,
                lang: language,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count >= 5);
            var userWithRoles = result.Data.First(u => u.Id == users[0].Id);
            var expectedRoleName = language == "ar" ? role.Name_ar : role.Name;
            Assert.Equal(expectedRoleName, userWithRoles.Roles);
        }

        [Fact]
        public async Task UpdateUserRolesAsync_WithValidRoles_UpdatesUserRoles()
        {
            // Arrange
            var user = TestDataGenerators.UserFaker().Generate();
            await _userManager.CreateAsync(user);

            var role1 = TestDataGenerators.RoleFaker().Generate();
            var role2 = TestDataGenerators.RoleFaker().Generate();
            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);


            // Initially assign role1
            await _userManager.AddToRoleAsync(user, role1.Name);

            // Act - Update to role2
            await _identityService.UpdateUserRolesAsync(user.Id, new[] { role2.Id }, CancellationToken.None);

            // Assert
            var userRoles = await _userManager.GetRolesAsync(user);
            Assert.Contains(role2.Name, userRoles);
            Assert.DoesNotContain(role1.Name, userRoles);
        }

        [Fact]
        public async Task UpdateUserRolesAsync_WithEmptyRoles_RemovesAllUserRoles()
        {
            // Arrange
            var user = TestDataGenerators.UserFaker().Generate();
            await _userManager.CreateAsync(user);

            var role = TestDataGenerators.RoleFaker().Generate();
            await _roleManager.CreateAsync(role);

            await _userManager.AddToRoleAsync(user, role.Name);

            // Act - Remove all roles
            await _identityService.UpdateUserRolesAsync(user.Id, new int[] { }, CancellationToken.None);

            // Assert
            var userRoles = await _userManager.GetRolesAsync(user);
            Assert.Empty(userRoles);
        }

        [Fact]
        public async Task UpdateUserRolesAsync_WithMultipleRoles_AssignsAllRoles()
        {
            // Arrange
            var user = TestDataGenerators.UserFaker().Generate();
            await _userManager.CreateAsync(user);

            var roles = TestDataGenerators.RoleFaker().Generate(3);
            await DbContext.Set<Role>().AddRangeAsync(roles);
            await UnitOfWork.SaveChangesAsync();

            // Act - Assign multiple roles
            await _identityService.UpdateUserRolesAsync(user.Id, roles.Select(r => r.Id).ToList(), CancellationToken.None);

            // Assert
            var userRoles = await _userManager.GetRolesAsync(user);
            Assert.All(roles, role => Assert.Contains(role.Name, userRoles));
            Assert.Equal(3, userRoles.Count);
        }

        [Fact]
        public async Task AddRolesAsync_WithValidRoles_CreatesRoles()
        {
            // Arrange
            var rolesToAdd = new List<RoleDto>
            {
                new RoleDto { Name_en = "TestRole1", Name_ar = "دور اختبار 1" },
                new RoleDto { Name_en = "TestRole2", Name_ar = "دور اختبار 2" },
                new RoleDto { Name_en = "TestRole3", Name_ar = "دور اختبار 3" }
            };

            // Act
            await _identityService.AddRolesAsync(rolesToAdd, CancellationToken.None);
            await UnitOfWork.SaveChangesAsync();

            // Assert
            var createdRoles = await _roleManager.Roles
                .ToListAsync();

            Assert.Equal(3, createdRoles.Count);
            Assert.Contains(createdRoles, r => r.Name == "TestRole1" && r.Name_ar == "دور اختبار 1");
            Assert.Contains(createdRoles, r => r.Name == "TestRole2" && r.Name_ar == "دور اختبار 2");
            Assert.Contains(createdRoles, r => r.Name == "TestRole3" && r.Name_ar == "دور اختبار 3");
        }

        [Fact]
        public async Task AddRolesAsync_WithSingleRole_CreatesRole()
        {
            // Arrange
            var roleToAdd = new List<RoleDto>
            {
                new RoleDto { Name_en = "SingleRole", Name_ar = "دور واحد" }
            };

            // Act
            await _identityService.AddRolesAsync(roleToAdd, CancellationToken.None);
            await UnitOfWork.SaveChangesAsync();

            // Assert
            var createdRole = await _roleManager.FindByNameAsync("SingleRole");
            Assert.NotNull(createdRole);
            Assert.Equal("SingleRole", createdRole.Name);
            Assert.Equal("دور واحد", createdRole.Name_ar);
            Assert.Equal("SINGLEROLE", createdRole.NormalizedName);
        }
    }
}
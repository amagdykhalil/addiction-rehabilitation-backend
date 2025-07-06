using ARC.Application.Abstractions.Repositories;
using ARC.Application.Features.Patients.Queries.GetPatients;
using ARC.Domain.Entities;
using ARC.Domain.Enums;
using ARC.IntegrationTests.PersistanceTests.Database.Common;
using ARC.Tests.Common.DataGenerators;
using ARC.Application.Common.Models;

namespace ARC.IntegrationTests.PersistanceTests.Database.Repositories
{
    public class PatientRepositoryTests : BaseDatabaseTests
    {
        private readonly IPatientRepository _patientRepository;

        public PatientRepositoryTests(DatabaseTestEnvironmentFixture fixture) : base(fixture)
        {
            _patientRepository = ServiceProvider.GetRequiredService<IPatientRepository>();
        }

        [Fact]
        public async Task GetPatientsAsync_Basic_ReturnsPagedPatients()
        {
            // Arrange
            var country = TestDataGenerators.CountryFaker.Generate();
            await DbContext.Set<Country>().AddAsync(country);
            await DbContext.SaveChangesAsync();

            var patients = TestDataGenerators.PatientFaker().Generate(5);
            foreach (var patient in patients)
            {
                patient.Person.Nationality = country;
            }
            await DbContext.Set<Patient>().AddRangeAsync(patients);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _patientRepository.GetPatientsAsync(
                searchQuery: null,
                gender: null,
                nationalityId: null,
                sortBy: PatientSortBy.Id,
                sortDirection: SortDirection.Asc,
                pageNumber: 1,
                pageSize: 10,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCount);
            Assert.Equal(5, result.Data.Count);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.PageSize);
        }

        [Fact]
        public async Task GetPatientsAsync_WithSearchQuery_FiltersResults()
        {
            // Arrange
            var country = TestDataGenerators.CountryFaker.Generate();
            await DbContext.Set<Country>().AddAsync(country);
            await DbContext.SaveChangesAsync();

            var person1 = TestDataGenerators.PersonFaker(country).Generate();
            person1.FirstName = "John";
            var patient1 = TestDataGenerators.PatientFaker(person1).Generate();

            var person2 = TestDataGenerators.PersonFaker(country).Generate();
            person2.FirstName = "Jane";
            var patient2 = TestDataGenerators.PatientFaker(person2).Generate();

            await DbContext.Set<Patient>().AddRangeAsync(patient1, patient2);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _patientRepository.GetPatientsAsync(
                searchQuery: "John",
                gender: null,
                nationalityId: null,
                sortBy: PatientSortBy.Id,
                sortDirection: SortDirection.Asc,
                pageNumber: 1,
                pageSize: 10,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalCount);
            Assert.Single(result.Data);
            Assert.Equal("John", result.Data.First().Person.FirstName);
        }

        [Fact]
        public async Task GetPatientsAsync_WithGenderFilter_FiltersResults()
        {
            // Arrange
            var country = TestDataGenerators.CountryFaker.Generate();
            await DbContext.Set<Country>().AddAsync(country);
            await DbContext.SaveChangesAsync();

            var person1 = TestDataGenerators.PersonFaker(country).Generate();
            person1.Gender = enGender.Male;
            var patient1 = TestDataGenerators.PatientFaker(person1).Generate();

            var person2 = TestDataGenerators.PersonFaker(country).Generate();
            person2.Gender = enGender.Female;
            var patient2 = TestDataGenerators.PatientFaker(person2).Generate();

            await DbContext.Set<Patient>().AddRangeAsync(patient1, patient2);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _patientRepository.GetPatientsAsync(
                searchQuery: null,
                gender: enGender.Male,
                nationalityId: null,
                sortBy: PatientSortBy.Id,
                sortDirection: SortDirection.Asc,
                pageNumber: 1,
                pageSize: 10,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalCount);
            Assert.Single(result.Data);
            Assert.Equal(enGender.Male, result.Data.First().Person.Gender);
        }

        [Fact]
        public async Task GetPatientsAsync_WithSortingAndPagination_WorksCorrectly()
        {
            // Arrange
            var country = TestDataGenerators.CountryFaker.Generate();
            await DbContext.Set<Country>().AddAsync(country);
            await DbContext.SaveChangesAsync();

            var patients = TestDataGenerators.PatientFaker().Generate(5);
            foreach (var patient in patients)
            {
                patient.Person.Nationality = country;
            }
            await DbContext.Set<Patient>().AddRangeAsync(patients);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _patientRepository.GetPatientsAsync(
                searchQuery: null,
                gender: null,
                nationalityId: null,
                sortBy: PatientSortBy.Id,
                sortDirection: SortDirection.Desc,
                pageNumber: 2,
                pageSize: 2,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCount);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal(2, result.CurrentPage);
            Assert.Equal(2, result.PageSize);
        }
    }
} 
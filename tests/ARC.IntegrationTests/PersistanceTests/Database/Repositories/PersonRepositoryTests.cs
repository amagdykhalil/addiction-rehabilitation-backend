using ARC.IntegrationTests.PersistanceTests.Database.Common;

namespace ARC.IntegrationTests.PersistanceTests.Database.Repositories
{

    public class PersonRepositoryTests : BaseDatabaseTests
    {
        private readonly IPersonRepository _personRepository;

        public PersonRepositoryTests(DatabaseTestEnvironmentFixture fixture) : base(fixture)
        {
            _personRepository = ServiceProvider.GetRequiredService<IPersonRepository>();
        }

        [Fact]
        public async Task AddAsync_CreatesPerson()
        {
            // Arrange
            var person = TestDataGenerators.PersonFaker().Generate();

            // Act
            await _personRepository.AddAsync(person);
            await UnitOfWork.SaveChangesAsync();

            // Assert
            Assert.NotEqual(0, person.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsPerson()
        {
            // Arrange
            var person = TestDataGenerators.PersonFaker().Generate();
            await _personRepository.AddAsync(person);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var foundPerson = await _personRepository.GetByIdAsync(person.Id);

            // Assert
            Assert.NotNull(foundPerson);
            Assert.Equal(person.FirstName, foundPerson.FirstName);
            Assert.Equal(person.LastName, foundPerson.LastName);
            Assert.Equal(person.NationalityId, foundPerson.NationalityId);
        }
    }
}
namespace ARC.Application.Abstractions.Persistence
{
    public interface ICountryRepository : IGenericRepository<Country>, IRepository
    {
        Task<List<string>> GetAllCountryNamesAsync(string language, CancellationToken cancellationToken);
        Task<List<Features.Countries.Queries.GetAll.CountryDto>> GetAllAsync(string lang, CancellationToken cancellationToken);
    }
}
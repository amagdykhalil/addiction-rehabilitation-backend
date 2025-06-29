using ARC.Application.Abstractions.Persistence;

namespace ARC.Application.Features.Countries.Queries.GetAll
{
    public class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, List<CountryDto>>
    {
        private readonly ICountryRepository _countryRepository;
        public GetAllCountriesQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Result<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var countries = await _countryRepository.GetAllAsync(lang, cancellationToken);
            return Result.Success(countries);
        }
    }
}
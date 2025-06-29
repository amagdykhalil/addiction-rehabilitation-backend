using Application.Abstractions.Messaging;

namespace ARC.Application.Features.Countries.Queries.GetAll
{
    public record GetAllCountriesQuery() : IQuery<List<CountryDto>>;
} 
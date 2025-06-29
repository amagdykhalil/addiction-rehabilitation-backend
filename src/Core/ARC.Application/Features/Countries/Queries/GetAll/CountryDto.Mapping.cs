using ARC.Domain.Entities;

namespace ARC.Application.Features.Countries.Queries.GetAll
{
    public static class CountryDtoMappingExtensions
    {
        public static CountryDto ToDto(this Country country, string lang)
        {
            return new CountryDto
            {
                Id = country.Id,
                Name = lang == "ar" ? country.Name_ar : country.Name_en,
                Iso3 = country.Iso3,
                Code = country.Code
            };
        }
    }
} 
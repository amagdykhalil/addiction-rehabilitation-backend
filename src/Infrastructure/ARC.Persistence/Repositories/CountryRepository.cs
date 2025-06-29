using ARC.Domain.Entities;
using ARC.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ARC.Application.Features.Countries.Queries.GetAll;

namespace ARC.Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository 
    {
        private readonly AppDbContext _context;
        public CountryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllCountryNamesAsync(string language, CancellationToken cancellationToken)
        {
            if (language?.ToLower() == "ar")
                return await _context.Countries.Select(c => c.Name_ar).ToListAsync(cancellationToken);
            else
                return await _context.Countries.Select(c => c.Name_en).ToListAsync(cancellationToken);
        }

        public async Task<List<CountryDto>> GetAllAsync(string lang, CancellationToken cancellationToken)
        {
            if (lang?.ToLower() == "ar")
            {
                return await _context.Countries
                    .Select(c => new CountryDto
                    {
                        Id = c.Id,
                        Name = c.Name_ar,
                        Iso3 = c.Iso3,
                        Code = c.Code
                    })
                    .ToListAsync(cancellationToken);
            }
            else
            {
                return await _context.Countries
                    .Select(c => new CountryDto
                    {
                        Id = c.Id,
                        Name = c.Name_en,
                        Iso3 = c.Iso3,
                        Code = c.Code
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
} 
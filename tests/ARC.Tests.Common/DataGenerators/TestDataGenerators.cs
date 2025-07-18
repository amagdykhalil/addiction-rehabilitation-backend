using ARC.Domain.Entities;
using ARC.Domain.Enums;
using Bogus;

namespace ARC.Tests.Common.DataGenerators
{
    public static class TestDataGenerators
    {
        public static Faker<Country> CountryFaker { get; } = new Faker<Country>()
            .RuleFor(c => c.Id, f => 0)
            .RuleFor(c => c.Name_en, f => $"{f.Address.Country()}_{f.UniqueIndex}")
            .RuleFor(c => c.Name_ar, f => $"{f.Address.Country()}_{f.UniqueIndex}")
            .RuleFor(c => c.Iso3, f => $"{f.Address.CountryCode()}{f.UniqueIndex}")
            .RuleFor(c => c.Code, (f, c) => c.Iso3);

        public static Faker<Domain.Entities.Person> PersonFaker(Country? country = null) => new Faker<Domain.Entities.Person>()
            .RuleFor(p => p.Id, f => 0)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.SecondName, f => f.Name.FirstName())
            .RuleFor(p => p.ThirdName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Gender, f => f.PickRandom<enGender>())
            .RuleFor(p => p.CallPhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.NationalIdNumber, f => $"1234{f.UniqueIndex:D10}")
            .RuleFor(p => p.PassportNumber, f => $"P{f.UniqueIndex:D9}")
            .RuleFor(p => p.Nationality, f => country ?? CountryFaker.Generate())
            .RuleFor(p => p.PersonalImageURL, f => f.Image.LoremFlickrUrl());

        public static Faker<User> UserFaker(Domain.Entities.Person? person = null) => new Faker<User>()
            .RuleFor(u => u.Id, f => 0)
            .RuleFor(u => u.UserName, f => $"user_{f.UniqueIndex}")
            .RuleFor(u => u.Email, (f, u) => $"{u.UserName}@example.com")
            .RuleFor(u => u.EmailConfirmed, f => true)
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.PhoneNumberConfirmed, f => true)
            .RuleFor(u => u.PasswordHash, f => "PasswordHash")
            .RuleFor(u => u.TwoFactorEnabled, f => false)
            .RuleFor(u => u.LockoutEnd, f => null as DateTimeOffset?)
            .RuleFor(u => u.LockoutEnabled, f => false)
            .RuleFor(u => u.AccessFailedCount, f => 0)
            .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
            .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
            .RuleFor(u => u.SecurityStamp, f => f.Random.Guid().ToString())
            .RuleFor(u => u.ConcurrencyStamp, f => f.Random.Guid().ToString())
            .RuleFor(u => u.Person, f => person ?? PersonFaker().Generate());

        public static Faker<RefreshToken> RefreshTokenFaker(User? user = null) => new Faker<RefreshToken>()
            .RuleFor(r => r.Id, f => 0)
            .RuleFor(r => r.Token, f => f.Random.AlphaNumeric(32))
            .RuleFor(r => r.User, f => user ?? UserFaker().Generate())
            .RuleFor(r => r.CreatedOn, f => DateTime.UtcNow)
            .RuleFor(r => r.ExpiresOn, f => DateTime.UtcNow.AddDays(7))
            .RuleFor(r => r.RevokedOn, f => null as DateTime?);

        public static Faker<Role> RoleFaker() => new Faker<Role>()
            .RuleFor(r => r.Id, f => 0)
            .RuleFor(r => r.Name, f => $"Role_{f.UniqueIndex}")         // unique
            .RuleFor(r => r.Name_ar, f => $"دور_{f.UniqueIndex}")       // unique
            .RuleFor(r => r.NormalizedName, (f, r) => r.Name.ToUpperInvariant())
            .RuleFor(r => r.ConcurrencyStamp, f => f.Random.Guid().ToString());

        public static Faker<Patient> PatientFaker(Domain.Entities.Person? person = null) => new Faker<Patient>()
            .RuleFor(p => p.Id, f => 0)
            .RuleFor(p => p.PersonId, f => 0) // Will be set when person is assigned
            .RuleFor(p => p.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(80, DateTime.Now.AddYears(-18))))
            .RuleFor(p => p.Person, f => person ?? PersonFaker().Generate());
    }
}
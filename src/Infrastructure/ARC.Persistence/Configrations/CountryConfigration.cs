using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class CountryConfigration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Name_ar).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Iso3).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Code).IsRequired().HasMaxLength(10);
        }
    }
}
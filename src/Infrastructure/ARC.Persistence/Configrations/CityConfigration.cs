using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class CityConfigration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Name_ar).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.State)
                   .WithMany(s => s.Cities)
                   .HasForeignKey(c => c.StateId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
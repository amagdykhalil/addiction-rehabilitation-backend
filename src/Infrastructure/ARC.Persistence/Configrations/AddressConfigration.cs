using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class AddressConfigration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            builder.Property(a => a.PostalCode).HasMaxLength(20);

            builder.HasOne(a => a.City)
                   .WithMany(c => c.Addresses)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
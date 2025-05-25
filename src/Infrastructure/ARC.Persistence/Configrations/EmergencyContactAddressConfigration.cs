using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class EmergencyContactAddressConfigration : IEntityTypeConfiguration<EmergencyContactAddress>
    {
        public void Configure(EntityTypeBuilder<EmergencyContactAddress> builder)
        {
            builder.ToTable("EmergencyContactAddresses");
            builder.HasKey(ea => ea.Id);
            builder.Property(ea => ea.Notes).HasMaxLength(500);

            builder.HasOne(ea => ea.Address)
                   .WithOne()
                   .HasForeignKey<EmergencyContactAddress>(ea => ea.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
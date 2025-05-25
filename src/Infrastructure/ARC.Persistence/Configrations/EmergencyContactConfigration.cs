using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class EmergencyContactConfigration : IEntityTypeConfiguration<EmergencyContact>
    {
        public void Configure(EntityTypeBuilder<EmergencyContact> builder)
        {
            builder.ToTable("EmergencyContacts");
            builder.HasKey(ec => ec.Id);
            builder.Property(ec => ec.Notes).HasMaxLength(500);

            builder.HasOne(ec => ec.EmergentPerson)
                   .WithOne()
                   .HasForeignKey<EmergencyContact>(ec => ec.EmergentPersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ec => ec.ContactAddress)
                   .WithOne()
                   .HasForeignKey<EmergencyContact>(ec => ec.ContactAddressId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
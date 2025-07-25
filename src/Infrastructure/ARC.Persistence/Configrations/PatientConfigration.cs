using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ARC.Persistence.Configrations
{
    public class PatientConfigration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Person)
                   .WithOne()
                   .HasForeignKey<Patient>(p => p.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Apply soft delete filter
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
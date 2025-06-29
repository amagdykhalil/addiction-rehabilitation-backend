using ARC.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class PersonConfigration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p => p.Id);


            builder.Property(p => p.NationalIdNumber).HasMaxLength(50);
            builder.Property(p => p.PassportNumber).HasMaxLength(50);
            builder.Property(p => p.PersonalImageURL).HasMaxLength(255);

            // Unique nullable constraint for NationalIdNumber
            builder.HasIndex(p => p.NationalIdNumber)
                   .IsUnique();

            // Unique nullable constraint for PassportNumber
            builder.HasIndex(p => p.PassportNumber)
                   .IsUnique();


            builder.Property(p => p.Gender)
                   .HasComment("stores 1 for Female, 0 for Male")
                   .HasConversion(
                        v => v == enGender.Female,
                        v => v ? enGender.Female : enGender.Male);

            builder.HasOne(p => p.Nationality)
                   .WithMany(c => c.People)
                   .HasForeignKey(p => p.NationalityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


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


            builder.Property(p => p.Gender)
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


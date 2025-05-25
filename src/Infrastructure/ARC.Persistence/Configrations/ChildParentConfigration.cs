using ARC.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ChildParentConfigration : IEntityTypeConfiguration<ChildParent>
    {
        public void Configure(EntityTypeBuilder<ChildParent> builder)
        {
            builder.ToTable("ChildParents");
            builder.HasKey(cp => cp.Id);


            builder.Property(cp => cp.ParentType)
                   .HasConversion(
                        v => v == enParentType.Mother,
                        v => v ? enParentType.Mother : enParentType.Father);

            builder.Property(cp => cp.Notes).HasMaxLength(500);
            builder.HasOne(cp => cp.ParentPerson)
                   .WithOne()
                   .HasForeignKey<ChildParent>(cp => cp.ParentPersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.EmploymentStatus)
                   .WithMany()
                   .HasForeignKey(cp => cp.EmploymentStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
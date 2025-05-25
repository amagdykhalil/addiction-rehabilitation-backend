using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class EmploymentStatusConfigration : IEntityTypeConfiguration<EmploymentStatus>
    {
        public void Configure(EntityTypeBuilder<EmploymentStatus> builder)
        {
            builder.ToTable("EmploymentStatuses");
            builder.HasKey(p => p.Id);

            builder.Property(s => s.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Name_ar).IsRequired().HasMaxLength(100);

        }
    }
}
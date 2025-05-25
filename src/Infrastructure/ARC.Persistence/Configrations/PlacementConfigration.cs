using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ARC.Persistence.Entities;

namespace ARC.Persistence.Configrations
{
    public class PlacementConfigration : IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.ToTable("Placements");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name_ar).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
} 
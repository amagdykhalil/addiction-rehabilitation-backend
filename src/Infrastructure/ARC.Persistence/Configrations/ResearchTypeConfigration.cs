using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchTypeConfigration : IEntityTypeConfiguration<ResearchType>
    {
        public void Configure(EntityTypeBuilder<ResearchType> builder)
        {
            builder.ToTable("ResearchTypes");
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Name_ar).IsRequired().HasMaxLength(100);
            builder.Property(rt => rt.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(rt => rt.Notes).HasMaxLength(500);

            // Self-referencing relationship
            builder.HasOne(rt => rt.ParentResearchType)
                   .WithMany(rt => rt.ChildResearchTypes)
                   .HasForeignKey(rt => rt.ParentResearchTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
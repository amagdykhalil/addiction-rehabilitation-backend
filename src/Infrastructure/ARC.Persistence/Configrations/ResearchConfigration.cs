using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchConfigration : IEntityTypeConfiguration<Research>
    {
        public void Configure(EntityTypeBuilder<Research> builder)
        {
            builder.ToTable("Researches");
            builder.HasKey(r => r.Id);

            // Relationships
            builder.HasOne(r => r.AdmissionAssessment)
                   .WithMany()
                   .HasForeignKey(r => r.AdmissionAssessmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.ResearchType)
                   .WithMany(rt => rt.Researches)
                   .HasForeignKey(r => r.ResearchTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
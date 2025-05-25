using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class PatientPlacementConfigration : IEntityTypeConfiguration<PatientPlacement>
    {
        public void Configure(EntityTypeBuilder<PatientPlacement> builder)
        {
            builder.ToTable("PatientPlacements");
            builder.HasKey(pp => pp.Id);
            builder.Property(pp => pp.Notes).HasMaxLength(500);

            // Relationships
            builder.HasOne(pp => pp.AdmissionAssessment)
                   .WithMany()
                   .HasForeignKey(pp => pp.AdmissionAssessmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pp => pp.Placement)
                   .WithMany(p => p.PatientPlacements)
                   .HasForeignKey(pp => pp.PlacementId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
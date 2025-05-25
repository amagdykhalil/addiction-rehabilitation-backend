using ARC.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class AdmissionAssessmentConfigration : IEntityTypeConfiguration<AdmissionAssessment>
    {
        public void Configure(EntityTypeBuilder<AdmissionAssessment> builder)
        {
            builder.ToTable("AdmissionAssessments");
            builder.HasKey(aa => aa.Id);
            builder.Property(aa => aa.InterviewDate).IsRequired();
            builder.Property(aa => aa.MaritalStatus).HasMaxLength(50);
            builder.Property(aa => aa.EducationalLevel).HasMaxLength(50);

            builder.Property(p => p.MaritalStatus)
                    .HasConversion(
                         v => (byte)v,
                         v => (enMaritalStatus)v);

            builder.Property(p => p.EducationalLevel)
                    .HasConversion(
                            v => (byte)v,
                            v => (enEducationalLevel)v);


            // Relationships
            builder.HasOne(aa => aa.Center)
                   .WithMany()
                   .HasForeignKey(aa => aa.CenterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aa => aa.Patient)
                   .WithMany()
                   .HasForeignKey(aa => aa.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aa => aa.EmploymentStatus)
                   .WithMany()
                   .HasForeignKey(aa => aa.EmploymentStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aa => aa.ResidenceInfo)
                   .WithOne()
                   .HasForeignKey<AdmissionAssessment>(aa => aa.ResidenceInfoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
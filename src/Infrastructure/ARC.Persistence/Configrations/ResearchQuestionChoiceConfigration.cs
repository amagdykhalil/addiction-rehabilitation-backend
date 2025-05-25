using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchQuestionChoiceConfigration : IEntityTypeConfiguration<ResearchQuestionChoice>
    {
        public void Configure(EntityTypeBuilder<ResearchQuestionChoice> builder)
        {
            builder.ToTable("ResearchQuestionChoices");
            builder.HasKey(rqc => rqc.Id);
            builder.Property(rqc => rqc.Choice_ar).IsRequired().HasMaxLength(200);
            builder.Property(rqc => rqc.Choice_en).IsRequired().HasMaxLength(200);
            builder.Property(rqc => rqc.Notes).HasMaxLength(500);

            // Relationship to QuestionVersion
            builder.HasOne(rqc => rqc.QuestionVersion)
                   .WithMany(qv => qv.ResearchQuestionChoices)
                   .HasForeignKey(rqc => rqc.QuestionVersionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
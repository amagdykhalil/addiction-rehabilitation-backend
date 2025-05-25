using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchAnswerChoiceConfigration : IEntityTypeConfiguration<ResearchAnswerChoice>
    {
        public void Configure(EntityTypeBuilder<ResearchAnswerChoice> builder)
        {
            builder.ToTable("ResearchAnswerChoices");
            builder.HasKey(rac => rac.Id);

            // Relationships
            builder.HasOne(rac => rac.ResearchQuestionAnswer)
                   .WithMany(rqa => rqa.ResearchAnswerChoices)
                   .HasForeignKey(rac => rac.ResearchQuestionAnswerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rac => rac.ResearchQuestionChoice)
                   .WithMany(rqc => rqc.ResearchAnswerChoices)
                   .HasForeignKey(rac => rac.ResearchQuestionChoiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
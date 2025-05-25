using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchQuestionAnswersConfigration : IEntityTypeConfiguration<ResearchQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<ResearchQuestionAnswer> builder)
        {
            builder.ToTable("ResearchQuestionAnswers");
            builder.HasKey(rqa => rqa.Id);
            builder.Property(rqa => rqa.IsDependant).IsRequired(); // BIT
            builder.Property(rqa => rqa.NumericAnswer).IsRequired(false);
            builder.Property(rqa => rqa.FreeText).HasMaxLength(2000).IsRequired(false);
            builder.Property(rqa => rqa.BooleanAnswer).IsRequired(false); // BIT

            // Relationships
            builder.HasOne(rqa => rqa.Research)
                   .WithMany(r => r.ResearchQuestionAnswers)
                   .HasForeignKey(rqa => rqa.ResearchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rqa => rqa.QuestionVersion)
                   .WithMany(qv => qv.ResearchQuestionAnswers)
                   .HasForeignKey(rqa => rqa.QuestionVersionId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(rqa => rqa.Choice)
                   .WithMany()
                   .HasForeignKey(rqa => rqa.ChoiceId)
                   .IsRequired(false);


        }
    }
}
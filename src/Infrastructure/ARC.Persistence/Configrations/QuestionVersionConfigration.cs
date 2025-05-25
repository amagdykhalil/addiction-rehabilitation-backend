using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class QuestionVersionConfigration : IEntityTypeConfiguration<QuestionVersion>
    {
        public void Configure(EntityTypeBuilder<QuestionVersion> builder)
        {
            builder.ToTable("QuestionVersions");
            builder.HasKey(qv => qv.Id);
            builder.Property(qv => qv.StartDate).IsRequired();
            builder.Property(qv => qv.EndDate).IsRequired(false);
            builder.Property(qv => qv.AnswerType).IsRequired().HasMaxLength(50);
            builder.Property(qv => qv.IsRequired).IsRequired();

            // Relationships
            builder.HasOne(qv => qv.ResearchQuestion)
                   .WithMany(rq => rq.QuestionVersions)
                   .HasForeignKey(qv => qv.ResearchQuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(qv => qv.ParentChoice)
                   .WithMany()
                   .HasForeignKey(qv => qv.ParentChoiceId)
                   .IsRequired(false);
        }
    }
}
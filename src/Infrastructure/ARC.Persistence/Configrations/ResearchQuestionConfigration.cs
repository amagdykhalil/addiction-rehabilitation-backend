using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class ResearchQuestionConfigration : IEntityTypeConfiguration<ResearchQuestion>
    {
        public void Configure(EntityTypeBuilder<ResearchQuestion> builder)
        {
            builder.ToTable("ResearchQuestions");
            builder.HasKey(rq => rq.Id);
            builder.Property(rq => rq.QuestionText_ar).IsRequired().HasMaxLength(500);
            builder.Property(rq => rq.QuestionText_en).IsRequired().HasMaxLength(500);

            // Relationships
            builder.HasOne(rq => rq.ResearchType)
                   .WithMany(rt => rt.ResearchQuestions)
                   .HasForeignKey(rq => rq.ResearchTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Self-referencing relationship
            builder.HasOne(rq => rq.ParentQuestion)
                   .WithMany(rq => rq.ChildQuestions)
                   .HasForeignKey(rq => rq.ParentQuestionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
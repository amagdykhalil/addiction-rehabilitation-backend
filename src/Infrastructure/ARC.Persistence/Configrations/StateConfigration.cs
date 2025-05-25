using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class StateConfigration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Name_ar).IsRequired().HasMaxLength(100);

            builder.HasOne(s => s.Country)
                   .WithMany()
                   .HasForeignKey(s => s.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
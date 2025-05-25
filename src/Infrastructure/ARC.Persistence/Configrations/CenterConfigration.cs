using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class CenterConfigration : IEntityTypeConfiguration<Center>
    {
        public void Configure(EntityTypeBuilder<Center> builder)
        {
            builder.ToTable("Centers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Name_en).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Name_ar).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.State)
                   .WithMany()
                   .HasForeignKey(c => c.StateId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.City)
                   .WithMany()
                   .HasForeignKey(c => c.CityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
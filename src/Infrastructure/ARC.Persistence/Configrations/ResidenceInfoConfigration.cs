using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ARC.Persistence.Entities;

namespace ARC.Persistence.Configrations
{
    public class ResidenceInfoConfigration : IEntityTypeConfiguration<ResidenceInfo>
    {
        public void Configure(EntityTypeBuilder<ResidenceInfo> builder)
        {
            builder.ToTable("ResidenceInfos");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.HasFixedResidence).IsRequired();
            builder.Property(r => r.Notes).HasMaxLength(500);
            
            builder.HasOne(r => r.Address)
                   .WithOne()
                   .HasForeignKey<ResidenceInfo>(r => r.AddressId)
                   .IsRequired(false);
        }
    }
} 
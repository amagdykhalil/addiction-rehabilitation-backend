using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistence.Configrations
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasOne(a => a.Person)
                   .WithOne()
                   .HasForeignKey<User>(a => a.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using ARC.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ARC.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmploymentStatus>().HasData(
                new EmploymentStatus { Id = 1, Name_ar = "عاطل عن العمل", Name_en = "Unemployed" },
                new EmploymentStatus { Id = 2, Name_ar = "عمل مستقل", Name_en = "Self Employed" },
                new EmploymentStatus { Id = 3, Name_ar = "يعمل ويدرس", Name_en = "Working & Studying" },
                new EmploymentStatus { Id = 4, Name_ar = "لا يعمل ويدرس", Name_en = "Not Working & Studying" },
                new EmploymentStatus { Id = 5, Name_ar = "ربة منزل", Name_en = "Homemaker" },
                new EmploymentStatus { Id = 6, Name_ar = "متقاعد", Name_en = "Retired" },
                new EmploymentStatus { Id = 7, Name_ar = "لا يوجد رد", Name_en = "No Response" }
            );

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "Receptionist",
                    NormalizedName = "RECEPTIONIST"
                }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Ahmed",
                    SecondName = "Magdy",
                    ThirdName = "Mostafa",
                    LastName = "Khalil",
                    Gender = enGender.Male,

                    CallPhoneNumber = "01148425889",
                    NationalIdNumber = "30225485672598",
                    NationalityId = 64, //Egypt 
                    PersonalImageURL = null
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "ahmed.magdy.dev9@gmail.com",
                    NormalizedEmail = "AHMED.MAGDY.DEV9@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEBCAwMGhwLxOSyC+U8pfBQy8SawEMXvexJEF5+QVFM5WCinzdOj2y1mcwO6FgaF3HA==", //Admin@123
                    SecurityStamp = "aa5a51aa-162d-4e9e-805c-c76645da10f2",
                    ConcurrencyStamp = "aa5a51aa-162d-4e9e-805c-c76645da10f2",
                    PhoneNumber = "01148425889",
                    PhoneNumberConfirmed = true,
                    PersonId = 1
                }
            );

            // Assign Admin role to user
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1 // Matches your Admin role seeded earlier
                }
            );

        }
    }
}

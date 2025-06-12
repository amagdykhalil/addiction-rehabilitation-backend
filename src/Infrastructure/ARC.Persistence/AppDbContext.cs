using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ARC.Persistence
{
    public class AppDbContext
        : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ChildParent> ChildParents { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<EmploymentStatus> EmploymentStatuses { get; set; }
        public DbSet<EmergencyContactAddress> EmergencyContactAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<ResidenceInfo> ResidenceInfos { get; set; }
        public DbSet<ResearchType> ResearchTypes { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<ResearchQuestion> ResearchQuestions { get; set; }
        public DbSet<QuestionVersion> QuestionVersions { get; set; }
        public DbSet<ResearchQuestionChoice> ResearchQuestionChoices { get; set; }
        public DbSet<ResearchAnswerChoice> ResearchAnswerChoices { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<ResearchQuestionAnswer> ResearchQuestionAnswers { get; set; }
        public DbSet<PatientPlacement> PatientPlacements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


    }
}




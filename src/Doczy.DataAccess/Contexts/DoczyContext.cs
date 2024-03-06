using Doczy.Core.Entities;
using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Doczy.DataAccess.Contexts
{
    public class DoczyContext : IdentityDbContext<BaseAppUser, IdentityRole<Guid>, Guid>
    {
        public DoczyContext(DbContextOptions<DoczyContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<DoctorAppUser> DoctorAppUsers { get; set; }
        public DbSet<PatientAppUser> PatientAppUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<DoctorCategory> DoctorCategories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Univercity> Univercities { get; set; }
        public DbSet<UnivercityDegree> UnivercityDegrees{ get; set; }
        public DbSet<FieldOfStudy> FieldOfStudies{ get; set; }
        public DbSet<Education> Educations{ get; set; }
        public DbSet<Experiance> Experiances{ get; set; }
        public DbSet<DoctorRating> DoctorRatings{ get; set; }
        public DbSet<FavoriteDoctor> FavoriteDoctors{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
             .HasOne(r => r.Doctor)
             .WithMany(c=>c.Appointments)
             .HasForeignKey(r => r.DoctorId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
            .HasOne(r => r.Patient)
            .WithMany(c=>c.Appointments)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<DoctorRating>()
            .HasOne(r => r.Doctor)
            .WithMany(c => c.Ratings)
            .HasForeignKey(r => r.DoctorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<DoctorRating>()
            .HasOne(r => r.Patient)
            .WithMany(c => c.Ratings)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FavoriteDoctor>()
           .HasOne(r => r.Patient)
           .WithMany(c => c.FavoriteDoctors)
           .HasForeignKey(r => r.PatientId)
           .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FavoriteDoctor>()
            .HasOne(r => r.Doctor)
            .WithMany(c => c.FavoriteDoctors)
            .HasForeignKey(r => r.DoctorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseSectionEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {

                    case EntityState.Modified:
                        entry.Entity.UptadetAt = DateTime.Now;
                        entry.Entity.UpdatedBy = "admin";
                        break;
                    case EntityState.Added:
                        entry.Entity.UptadetAt = DateTime.Now;
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = "admin";
                        entry.Entity.UpdatedBy = "admin";
                        break;


                    default:
                        break;
                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}

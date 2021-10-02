using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.ApplicationContext
{
    public class SchoolDbContext : DbContext
    {

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<SchoolDbContext>();
            modelBuilder.ConfigureDeletableEntities();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            this.AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            this.AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess,
                cancellationToken);
        }

        private const string IsDeletedProperty = "IsDeleted";
        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[IsDeletedProperty] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[IsDeletedProperty] = true;
                        break;
                }
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<application> Applications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<Jamaat> Jamaats { get; set; }
    }
}

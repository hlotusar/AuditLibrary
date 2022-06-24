using AuditFramework;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2
{
    public partial class PostgresDemoContext : DbContext
    {
        private AuditRecordCreator _auditRecordCreator;

        public PostgresDemoContext()
        {
            _auditRecordCreator = new AuditRecordCreator(this);
        }

        public PostgresDemoContext(DbContextOptions<PostgresDemoContext> options)
            : base(options)
        {
            _auditRecordCreator = new AuditRecordCreator(this);
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
                string conn = configuration.GetConnectionString("PostgresDemoConnectionString");
                optionsBuilder.UseNpgsql(conn);
            }
        }

        public override int SaveChanges()
        {
            try
            {
                _auditRecordCreator.CreateAuditRecords();
            }
            catch (Exception ex)
            {

                
            }
            return base.SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("employees_pkey");

                entity.ToTable("employees");

                entity.Property(e => e.Empid)
                    .HasColumnName("empid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Empname)
                    .HasColumnType("character varying")
                    .HasColumnName("empname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

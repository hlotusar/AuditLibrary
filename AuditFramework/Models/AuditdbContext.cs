using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace AuditFramework.Models
{
    public partial class AuditdbContext : DbContext
    {
        public AuditdbContext()
        {
        }

        public AuditdbContext(DbContextOptions<AuditdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
                var conn = configuration.GetConnectionString("AuditdbConnectionString");
                optionsBuilder.UseNpgsql(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.Property(e => e.Auditid)
                    .HasColumnName("auditid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Actionname)
                    .HasColumnType("character varying")
                    .HasColumnName("actionname");

                entity.Property(e => e.Oldvalue)
                    .HasColumnType("character varying")
                    .HasColumnName("oldvalue");

                entity.Property(e => e.Propertyname)
                    .HasColumnType("character varying")
                    .HasColumnName("propertyname");

                entity.Property(e => e.Tablename)
                    .HasColumnType("character varying")
                    .HasColumnName("tablename");

                entity.Property(e => e.Updatedat)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

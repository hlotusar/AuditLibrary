using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Auditdb;User Id=postgres;Password=tusar@1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit");

                entity.Property(e => e.AuditId).ValueGeneratedNever();

                entity.Property(e => e.ActionName).HasColumnType("character varying");

                entity.Property(e => e.EntityId).HasColumnType("character varying");

                entity.Property(e => e.EntityType).HasColumnType("character varying");

                entity.Property(e => e.ModuleName).HasColumnType("character varying");

                entity.Property(e => e.NewValue).HasColumnType("character varying");

                entity.Property(e => e.Oldvalue).HasColumnType("character varying");

                entity.Property(e => e.PropertyName).HasColumnType("character varying");

                entity.Property(e => e.TransactionId).HasColumnType("character varying");

                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp without time zone");

                entity.Property(e => e.User).HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

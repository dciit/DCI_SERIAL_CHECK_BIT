using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DCI_SERIAL_CHECK_BIT.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DCI_SERIAL_CHECK_BIT.Contexts
{
    public partial class DBSCM : DbContext
    {
        public DBSCM()
        {
        }

        public DBSCM(DbContextOptions<DBSCM> options)
            : base(options)
        {
        }

        public virtual DbSet<AdjSerial> AdjSerial { get; set; }
        public virtual DbSet<AdjSerialEditDigit> AdjSerialEditDigit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.226.86;Database=dbSCM;TrustServerCertificate=True;uid=sa;password=decjapan");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdjSerial>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ADJ_SERIAL");

                entity.Property(e => e.AdjId).HasColumnName("ADJ_ID");

                entity.Property(e => e.AdjStatus).HasColumnName("ADJ_STATUS");

                entity.Property(e => e.EmCode)
                    .HasColumnName("EM_CODE")
                    .HasMaxLength(5);

                entity.Property(e => e.LineNew)
                    .HasColumnName("LINE_NEW")
                    .HasMaxLength(1);

                entity.Property(e => e.LineOld)
                    .HasColumnName("LINE_OLD")
                    .HasMaxLength(1);

                entity.Property(e => e.ModelCodeNew)
                    .HasColumnName("MODEL_CODE_NEW")
                    .HasMaxLength(4);

                entity.Property(e => e.ModelCodeOld)
                    .HasColumnName("MODEL_CODE_OLD")
                    .HasMaxLength(4);

                entity.Property(e => e.ModelNameNew)
                    .HasColumnName("MODEL_NAME_NEW")
                    .HasMaxLength(20);

                entity.Property(e => e.ModelNameOld)
                    .HasColumnName("MODEL_NAME_OLD")
                    .HasMaxLength(20);

                entity.Property(e => e.RefNo)
                    .HasColumnName("REF_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.SerialNew)
                    .HasColumnName("SERIAL_NEW")
                    .HasMaxLength(16);

                entity.Property(e => e.SerialOld)
                    .HasColumnName("SERIAL_OLD")
                    .HasMaxLength(16);

                entity.Property(e => e.UpdateDt)
                    .HasColumnName("UPDATE_DT")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<AdjSerialEditDigit>(entity =>
            {
                entity.HasKey(e => new { e.Serial, e.SerialOld, e.SerialNew });

                entity.ToTable("ADJ_SERIAL_EDIT_DIGIT");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SerialOld)
                    .HasColumnName("serial_old")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNew)
                    .HasColumnName("serial_new")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SerialStatus).HasColumnName("serial_status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

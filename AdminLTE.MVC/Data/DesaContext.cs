using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AdminLTE.MVC.Models;

namespace AdminLTE.MVC.Data
{
    public partial class DesaContext : DbContext
    {
        public DesaContext()
        {
        }

        public DesaContext(DbContextOptions<DesaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Specialite> Specialites { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<StagePhase> StagePhases { get; set; }
        public virtual DbSet<Stagiaire> Stagiaires { get; set; }
        public virtual DbSet<StagiaireStage> StagiaireStages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source = data.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phase>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Specialite>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("TEXT(100)");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Promotion).IsRequired();
            });

            modelBuilder.Entity<StagePhase>(entity =>
            {
                entity.HasKey(e => new { e.StageId, e.PhaseId, e.SpecialileId });

                entity.HasIndex(e => e.PhaseId, "IX_StagePhases_PhaseId");

                entity.Property(e => e.AddedOn).IsRequired();

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.StagePhases)
                    .HasForeignKey(d => d.PhaseId);

                entity.HasOne(d => d.Specialile)
                    .WithMany(p => p.StagePhases)
                    .HasForeignKey(d => d.SpecialileId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.StagePhases)
                    .HasForeignKey(d => d.StageId);
            });

            modelBuilder.Entity<Stagiaire>(entity =>
            {
                entity.HasOne(d => d.Specialite)
                    .WithMany(p => p.Stagiaires)
                    .HasForeignKey(d => d.SpecialiteId);
            });

            modelBuilder.Entity<StagiaireStage>(entity =>
            {
                entity.HasKey(e => new { e.StagiaireId, e.StageId, e.SpecilaiteId });

                entity.Property(e => e.DateDebut).IsRequired();

                entity.Property(e => e.DateFin).IsRequired();

                entity.HasOne(d => d.Specilaite)
                    .WithMany(p => p.StagiaireStages)
                    .HasForeignKey(d => d.SpecilaiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Stagiaire)
                    .WithMany(p => p.StagiaireStages)
                    .HasForeignKey(d => d.StagiaireId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

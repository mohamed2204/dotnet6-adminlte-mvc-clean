using System.Reflection.Emit;
using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //entities
        public virtual DbSet<Matiere> Matieres { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Specialite> Specialites { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<StagePhase> StagePhases { get; set; }
        public virtual DbSet<Stagiaire> Stagiaires { get; set; }
        public virtual DbSet<StagiaireStage> StagiaireStages { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region sqlserver
            //builder.Entity<ApplicationUser>().ToTable("Users", "security");
            //builder.Entity<IdentityRole>().ToTable("Roles", "security");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            //builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            #endregion

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");


            builder.Entity<Phase>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            builder.Entity<Specialite>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("TEXT(100)");
            });

            builder.Entity<Stage>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Promotion).IsRequired();
            });

            builder.Entity<StagePhase>(entity =>
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

            builder.Entity<Stagiaire>(entity =>
            {
                entity.HasOne(d => d.Specialite)
                    .WithMany(p => p.Stagiaires)
                    .HasForeignKey(d => d.SpecialiteId);
            });

            builder.Entity<StagiaireStage>(entity =>
            {
                entity.HasKey(e => new { e.StagiaireId, e.StageId, e.SpecialiteId });
                entity.Property(e => e.DateDebut).IsRequired();
                entity.Property(e => e.DateFin).IsRequired();
                entity.HasOne(d => d.Specialite)
                    .WithMany(p => p.StagiaireStages)
                    .HasForeignKey(d => d.SpecialiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Stagiaire)
                    .WithMany(p => p.StagiaireStages)
                    .HasForeignKey(d => d.StagiaireId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //OnModelCreatingPartial(builder);

            #region template

            //builder.Entity<StagePhase>()
            //   .HasKey(sc => new { sc.StageId, sc.PhaseId }); // Composite Key for Join Table
            //builder.Entity<StagePhase>()
            //    .HasOne(sc => sc.Stage)
            //    .WithMany(s => s.StagePhases) // Navigation Property in Student
            //    .HasForeignKey(sc => sc.StageId); //Foreign Key
            //builder.Entity<StagePhase>()
            //    .HasOne(sc => sc.Phase)
            //    .WithMany(c => c.StagePhases) // Navigation Property in Course
            //    .HasForeignKey(sc => sc.PhaseId); //Foreign Key
            //// Explicitly configure the join table name
            //builder.Entity<StagePhase>().ToTable("StagePhases");
            #endregion
        }

        //partial void OnModelCreatingPartial(ModelBuilder builder);
    }
}

using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //entities
        public DbSet<Stagiaire> Students { get; set; }
        public DbSet<Matiere> Matieres { get; set; }

        public DbSet<Stage> Stages { get; set; }
        public DbSet<Phase> Phases { get; set; }
        //public DbSet<Stagiaire> Students { get; set; }
        //public DbSet<Matiere> Matieres { get; set; }


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
        }
    }
}

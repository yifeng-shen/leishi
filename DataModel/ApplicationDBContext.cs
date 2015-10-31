namespace leishi.DataModel
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;
    using Common;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDBContext : IdentityDbContext<AppUser, AppRole, string, AppLogin, AppUserRole, AppClaim>
    {
        static ApplicationDBContext()
        {
            Database.SetInitializer<ApplicationDBContext>(null);
        }

        public ApplicationDBContext()
            : base(GlobalConfiguration.DataBaseConnectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppClaim>().ToTable("AppClaims");
            modelBuilder.Entity<AppLogin>().ToTable("AppLogins");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRoles");
        }
    }
}

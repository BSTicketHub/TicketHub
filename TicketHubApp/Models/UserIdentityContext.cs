using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TicketHubApp.Models
{
    public class UserIdentityContext : IdentityDbContext<TicketHubUser>
    {
        public UserIdentityContext() : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public static UserIdentityContext Create()
        {
            return new UserIdentityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Custom Identity tables name
            //modelBuilder.Entity<TicketHubUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<TicketHubUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}

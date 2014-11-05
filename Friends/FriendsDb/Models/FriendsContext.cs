using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FriendsDb.Models.Mapping;

namespace FriendsDb.Models
{
    public partial class FriendsContext : DbContext
    {
        static FriendsContext()
        {
            Database.SetInitializer<FriendsContext>(null);
        }

        public FriendsContext()
            : base("Name=FriendsContext")
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserCredentialMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
        }
    }
}

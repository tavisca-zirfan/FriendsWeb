using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FriendsDb.Models.Mapping;

namespace FriendsDb.Models
{
    public partial class FriendsContext : DbContext
    {
        static FriendsContext()
        {
            Database.SetInitializer<FriendsContext>(new CreateDatabaseIfNotExists<FriendsContext>());
        }

        public FriendsContext()
            : base("FriendsContext")
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<EventInvited> EventInviteds { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new EventInvitedMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new EventTypeMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserCredentialMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
        }
    }
}

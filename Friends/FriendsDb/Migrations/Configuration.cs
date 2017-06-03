using FriendsDb.Models;

namespace FriendsDb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendsDb.Models.FriendsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendsDb.Models.FriendsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Roles.AddOrUpdate(r=>r.RoleId,new Role{RoleId = 1,RoleName = "Admin"},new Role{RoleId = 2,RoleName = "User"});
            context.EventTypes.AddOrUpdate(e => e.EventTypeId, new EventType { EventTypeId = 1, EventType1 = "Birthday" }, new EventType { EventTypeId = 2, EventType1 = "Marriage" }, new EventType { EventTypeId = 3, EventType1 = "Outing" });
        }
    }
}

using TODO.Domain.Core.Entities;

namespace TODO.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TODO.Data.Context.DataDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TODO.Data.Context.DataDbContext context)
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

            context.Assignments.AddOrUpdate(p => p.Name, 
                new Assignment {Name = "Do the dishes", Done = false, DueDate = DateTime.Now.AddDays(1)},
                new Assignment {Name = "Complete this app", Done = false, DueDate = DateTime.Now.AddDays(2)},
                new Assignment {Name = "Write an essae", Done = false, DueDate = DateTime.Now.AddDays(5)}
                );
        }
    }
}

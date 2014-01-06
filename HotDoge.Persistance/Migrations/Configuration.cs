namespace HotDoge.Persistence.Migrations
{
    using HotDoge.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotDoge.Persistence.DogeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotDoge.Persistence.DogeContext context)
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



            // Create some users and roles
            // TODO : change the passwords before deploying in production
            // please note that access to TestEntityController is tied to the role canAccessEntityController
            //AddUserAndRole(context, "canViewLogs", "admin", "123woof!"); // change default password !
            //AddUserAndRole(context, "otherRole", "polly", "wantsacracker"); // change default password !
            //AddUserAndRole(context, "canAccessEntityController", "bob", "test123!"); // change default password !


            // fill in TestEntity table
            var sdffsd = new List<TestEntity>
            {
                new TestEntity{LastName="Alexander"},
                new TestEntity{LastName="Bob"},
                new TestEntity{LastName="Ann"}
            };

            sdffsd.ForEach(s => context.TestEntitys.Add(s));
            context.SaveChanges();
        }


        bool AddUserAndRole(HotDoge.Persistence.DogeContext context, String roleName, String userName, String passWord)
        {
            IdentityResult ir;
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!RoleManager.RoleExists(roleName))
            {
                ir = RoleManager.Create(new IdentityRole(roleName));
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (UserManager.Find(userName, passWord) == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = userName,
                };
                ir = UserManager.Create(user, passWord);
                if (ir.Succeeded == false)
                    return ir.Succeeded;
                ir = UserManager.AddToRole(user.Id, roleName);
                return ir.Succeeded;
            }

            return false;
        }
    }
}
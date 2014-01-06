using Microsoft.AspNet.Identity.EntityFramework;
using HotDoge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HotDoge.Persistence
{
    /// <summary>
    /// Interface for our application dbContext. Allows to derive a FakeDogeContext to unit test the repositories.
    /// </summary>
    public interface IDogeContext : IDisposable
    {
        IDbSet<TestEntity> TestEntitys { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Save();
    }

    /// <summary>
    /// Actual dbContext used by the application. Implements IDogeContext interface, but also extends IdentityDbContext to allow for the
    /// Also specifies the name of the database code first deploys to
    /// 
    /// Code First commands (in Package Manager Console) :
    ///  Add-Migration -Project "HotDoge.Persistence" name
    ///  Update-Database -Project "HotDoge.Persistence"
    /// 
    /// </summary>
    public class DogeContext: IdentityDbContext<ApplicationUser>, IDogeContext
    {

        public DogeContext()
            : base("HotDogeDB") // <- Name of the database to deploy to
        {
        }

        public IDbSet<TestEntity> TestEntitys { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
using HotDoge.Entities;
using HotDoge.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDoge.Tests.Persistence
{
    /// <summary>
    /// Fake DBContext used to unit test the repository, holding empty implementations of interface methods. Methods of the context which are used in repository implementations must be "manually" simulated.
    /// ex : GenericRepository constructor uses context.Set<TEntity>() to set its "dbSet" field. In the unit test, the field is set by the MakeSut method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeDogeContext : IDogeContext
    {
        public IDbSet<TestEntity> TestEntitys { get; set; }

        public FakeDogeContext()
        {
            TestEntitys = new FakeDbSet<TestEntity>();
        }

        public void Dispose()
        {
            // nothing to dispose here
        }

        public void Save()
        {
            //
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return null;
        }


        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            //if (typeof(TEntity) == typeof(TestEntity))
            //{
            //    return (DbSet<TEntity>)TestEntitys; // unfortunately this cast does not work
            //}
            return null;
        }

    }
}

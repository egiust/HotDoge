using HotDoge.Persistence;
using HotDoge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HotDoge.Persistence.Interfaces
{
    /// <summary>
    /// Interface for the Repository for the TestEntity entity. Allows for dependancy injection in the business services classes.
    /// All those methods are implemented by the class GenericRepository.
    /// </summary>
    public interface ITestEntityRepository
    {                
        IEnumerable<TestEntity> Get(Expression<Func<TestEntity, bool>> filter = null, Func<IQueryable<TestEntity>, IOrderedQueryable<TestEntity>> orderBy = null, string includeProperties = "");
        TestEntity GetByID(object id);
        void Create(TestEntity newEntity);
        void Update(TestEntity entity);
        void Delete(TestEntity entity);
    }
}

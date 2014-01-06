using HotDoge.Persistence;
using HotDoge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDoge.Persistence.Interfaces;

namespace HotDoge.Persistence.Repositories
{
    /// <summary>
    /// Repository for the TestEntity entity. An interface is defined to allow for easy testing.
    /// Basic methods are implemented by the base class GenericRepository.
    /// No specific methods are defined here for the TestEntity repository. 
    /// </summary>
    public class TestEntityRepository : GenericRepository<TestEntity>, ITestEntityRepository
    {        
        public TestEntityRepository(IDogeContext context)
            : base(context)
        {
        } 
    }  
}

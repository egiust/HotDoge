using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDoge.Persistence;
using HotDoge.Entities;
using HotDoge.Persistence.Repositories;

namespace HotDoge.Business.Interfaces
{
    /// <summary>
    /// Interface for our demo service
    /// </summary>
    public interface ITestEntityService
    {
        List<TestEntity> GetAll();
        TestEntity GetById(int? id);
        void Create(TestEntity newEntity);
        void Update(TestEntity entity);
        void Delete(TestEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDoge.Persistence;
using HotDoge.Entities;
using HotDoge.Persistence.Interfaces;
using HotDoge.Business.Interfaces;

namespace HotDoge.Business.Services
{
    /// <summary>
    /// Our demo business service used in the scaffolded TestEntityController
    /// Note the use of Unit Of Work in methods that alter data
    /// </summary>
    public class TestEntityService : ITestEntityService 
    {
        private readonly ITestEntityRepository _repo;
        private readonly IUnitOfWork _uow;

        public TestEntityService(ITestEntityRepository testEntityRepository, IUnitOfWork uow)
        {
            if (testEntityRepository == null) throw new ArgumentNullException("testEntityRepository");
            if (uow == null) throw new ArgumentNullException("uow");
            _repo = testEntityRepository;
            _uow = uow;
        }

        public List<TestEntity> GetAll()
        {
            return _repo.Get(orderBy: q => q.OrderBy(t => t.LastName)).ToList();
        }


        public TestEntity GetById(int? id)
        {
            return _repo.GetByID(id);
        }


        public void Create(TestEntity newEntity)
        {
            _repo.Create(newEntity);
            _uow.Save();
        }

        public void Update(TestEntity entity)
        {
            _repo.Update(entity);
            _uow.Save();
        }

        public void Delete(TestEntity entity)
        {
            _repo.Delete(entity);
            _uow.Save();
        }
    }
}

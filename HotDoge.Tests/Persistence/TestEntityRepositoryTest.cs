using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotDoge.Persistence.Repositories;
using HotDoge.Persistence;
using HotDoge.Tests.Persistence;
using HotDoge.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace HotDoge.Tests
{
    /// <summary>
    /// Test class for the TestEntityRepository. Helper methods are used to arrange each test environment. Tests are not exhaustive.
    /// </summary>
    [TestClass]
    public class TestEntityRepositoryTest
    {
        #region Get

        [TestMethod]
        public void Get_with_no_params_and_no_data_must_return_empty()
        {
            // Arrange
            var sut = MakeSut();

            // Act
            var res = sut.Get().ToList();

            // Assert		
            Assert.AreEqual(0, res.Count, "should return 0 results");
        }

        [TestMethod]
        public void Get_with_no_params_and_some_data_must_return_data()
        {
            // Arrange
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" } };
            var sut = MakeSutWithExisting(data);

            // Act
            var res = sut.Get().ToList();

            // Assert
            Assert.AreEqual(2, res.Count, "should return data when unknown code is asked");
            Assert.AreEqual("Joe", res[0].LastName);
            Assert.AreEqual("Bob", res[1].LastName);
        }

        [TestMethod]
        public void Get_with_orderby_lastname_param_and_some_data_must_return_data_ordered_by_lastname()
        {
            // Arrange
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" } };
            var sut = MakeSutWithExisting(data);

            // Act
            var res = sut.Get(orderBy: a => a.OrderBy(d => d.LastName)).ToList();

            // Assert
            Assert.AreEqual(2, res.Count, "should return data when unknown code is asked");
            Assert.AreEqual("Bob", res[0].LastName);
            Assert.AreEqual("Joe", res[1].LastName);
        }
        
        #endregion


        #region GetById

        [TestMethod]
        public void GetById_existingentity_must_return_expected_entity()
        {
            // Arrange    

            var theEntity = new TestEntity { LastName = "nouv", Id = 12 };
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" }, theEntity };
            var sut = MakeSutWithExisting(data);

            // Act
            var result = sut.GetByID(12);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(theEntity.Id, result.Id);
            Assert.AreEqual(theEntity.LastName, result.LastName);
        }

        [TestMethod]
        public void GetById_nonexistingentity_must_return_expected_entity()
        {
            // Arrange    

            var theEntity = new TestEntity { LastName = "nouv", Id = 12 };
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" }, theEntity };
            var sut = MakeSutWithExisting(data);

            // Act
            var result = sut.GetByID(123);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetById_noentities_must_return_expected_entity()
        {
            // Arrange    
            var sut = MakeSut();

            // Act
            var result = sut.GetByID(123);

            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_existingentity_must_remove_expected_entity()
        {
            // Arrange    
            var fakeContext = new FakeDogeContext();
            var theEntity = new TestEntity { LastName = "nouv", Id = 12 };
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" }, theEntity };
            var sut = MakeSutWithExisting(fakeContext, data);

            // Act
            sut.Delete(theEntity);

            // Assert
            Assert.AreEqual(2, fakeContext.TestEntitys.Count(), "should return data when unknown code is asked");
            Assert.AreEqual(1, fakeContext.TestEntitys.ToList()[0].Id);
            Assert.AreEqual(2, fakeContext.TestEntitys.ToList()[1].Id);
        }

        [TestMethod]
        public void Delete_nonexistingentity_must_do_nothing()
        {
            // Arrange    
            var fakeContext = new FakeDogeContext();
            var theEntity = new TestEntity { LastName = "nouv", Id = 12 };
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" } };
            var sut = MakeSutWithExisting(fakeContext, data);

            // Act
            sut.Delete(theEntity);

            // Assert
            Assert.AreEqual(2, fakeContext.TestEntitys.Count(), "should return data when unknown code is asked");
            Assert.AreEqual(1, fakeContext.TestEntitys.ToList()[0].Id);
            Assert.AreEqual(2, fakeContext.TestEntitys.ToList()[1].Id);
        }
        
        #endregion


        #region Create

        [TestMethod]
        public void Create_nullentity_must_do_nothing()
        {
            // Arrange    
            var dbContext = new FakeDogeContext();
            var sut = MakeSut(dbContext);

            // Act
            sut.Create(null);

            // Assert
            Assert.AreEqual(0, dbContext.TestEntitys.Count());
        }

        [TestMethod]
        public void Create_testentity_must_add_data()
        {
            // Arrange    
            var dbContext = new FakeDogeContext();
            var sut = MakeSut(dbContext);
            var newEntity = new TestEntity { LastName = "nouv", Id = 12 };

            // Act
            sut.Create(newEntity);

            // Assert
            Assert.IsTrue(dbContext.TestEntitys.Contains(newEntity));
        }

        #endregion



        #region Update

        [TestMethod]
        public void Update_nullentity_must_do_nothing()
        {
            // Arrange    
            var dbContext = new FakeDogeContext();
            var sut = MakeSut(dbContext);

            // Act
            sut.Update(null);

            // Assert
            Assert.AreEqual(0, dbContext.TestEntitys.Count());
        }

        [TestMethod]
        public void Update_existing_testentity_must_modify_data()
        {
            // Arrange    
            var fakeContext = new FakeDogeContext();
            var theEntity = new TestEntity { LastName = "nouv", Id = 12 };
            TestEntity[] data = { new TestEntity { Id = 1, LastName = "Joe" }, new TestEntity { Id = 2, LastName = "Bob" }, theEntity };
            var sut = MakeSutWithExisting(fakeContext, data);

            // Act
            theEntity.LastName = "wow!";
            sut.Update(theEntity);
            var result = sut.GetByID(12);

            // Assert
            Assert.AreEqual(3, fakeContext.TestEntitys.Count(), "should return data when unknown code is asked");
            Assert.AreEqual("wow!", result.LastName);
        }

        #endregion

        #region Test Helper Methods

        private TestEntityRepository MakeSut(IDogeContext dbContext = null) //TODO : complete list of parameters
        {
            dbContext = dbContext ?? new FakeDogeContext();
            var repo = new TestEntityRepository(dbContext);
            repo.dbSet = dbContext.TestEntitys;
            return repo;
        }

        private TestEntityRepository MakeSutWithExisting(params TestEntity[] testEntities)
        {
            var fakeContext = new FakeDogeContext();
            fakeContext.TestEntitys = new FakeDbSet<TestEntity>(testEntities);                 
            return MakeSut(fakeContext);
        }

        private TestEntityRepository MakeSutWithExisting(FakeDogeContext context, params TestEntity[] testEntities)
        {            
            context.TestEntitys = new FakeDbSet<TestEntity>(testEntities);
            return MakeSut(context);
        }

        #endregion
    }
}

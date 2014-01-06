using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotDoge.Business.Interfaces;
using HotDoge.Business.Services;
using HotDoge.Persistence;
using HotDoge.Persistence.Interfaces;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;
using HotDoge.Entities;

namespace HotDoge.Tests.Business
{
    [TestClass]
    public class TestEntityServiceTest
    {
        [TestMethod]
        public void GetAll_must_call_repository_get_method()
        {
            //Arrange            
            var testEntityRepository = MockRepository.GenerateStrictMock<ITestEntityRepository>();
            testEntityRepository.Expect(d => d.Get()).IgnoreArguments()
                .Return(new List<TestEntity>());
            var sut = MakeSut(testEntityRepository);
            //Act
            var result = sut.GetAll();

            //Assert
            testEntityRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetById_must_call_repository_getbyid_method()
        {
            //Arrange            
            var testEntityRepository = MockRepository.GenerateStrictMock<ITestEntityRepository>();
            testEntityRepository.Expect(d => d.GetByID(13))
                .Return(new TestEntity());
            var sut = MakeSut(testEntityRepository);
            //Act
            var result = sut.GetById(13);

            //Assert
            testEntityRepository.VerifyAllExpectations();
        }


        [TestMethod]
        public void Create_must_call_repository_Create_method_and_uow_save()
        {
            //Arrange      
            var theEntity = new TestEntity() { Id = 5 };
            var testEntityRepository = MockRepository.GenerateStrictMock<ITestEntityRepository>();
            testEntityRepository.Expect(d => d.Create(theEntity));
            var uow = MockRepository.GenerateStrictMock<IUnitOfWork>();
            uow.Expect(u => u.Save());
            var sut = MakeSut(testEntityRepository, uow);
            
            //Act
            sut.Create(theEntity);

            //Assert
            testEntityRepository.VerifyAllExpectations();
            uow.VerifyAllExpectations();
        }


        [TestMethod]
        public void Update_must_call_repository_Update_method_and_uow_save()
        {
            //Arrange      
            var theEntity = new TestEntity() { Id = 5 };
            var testEntityRepository = MockRepository.GenerateStrictMock<ITestEntityRepository>();
            testEntityRepository.Expect(d => d.Update(theEntity));
            var uow = MockRepository.GenerateStrictMock<IUnitOfWork>();
            uow.Expect(u => u.Save());
            var sut = MakeSut(testEntityRepository, uow);

            //Act
            sut.Update(theEntity);

            //Assert
            testEntityRepository.VerifyAllExpectations();
            uow.VerifyAllExpectations();
        }


        [TestMethod]
        public void Delete_must_call_repository_Delete_method_and_uow_save()
        {
            //Arrange      
            var theEntity = new TestEntity() { Id = 5 };
            var testEntityRepository = MockRepository.GenerateStrictMock<ITestEntityRepository>();
            testEntityRepository.Expect(d => d.Delete(theEntity));
            var uow = MockRepository.GenerateStrictMock<IUnitOfWork>();
            uow.Expect(u => u.Save());
            var sut = MakeSut(testEntityRepository, uow);

            //Act
            sut.Delete(theEntity);

            //Assert
            testEntityRepository.VerifyAllExpectations();
            uow.VerifyAllExpectations();
        }

        #region Test Helper Methods
        private static ITestEntityService MakeSut(ITestEntityRepository testEntityRepository = null, IUnitOfWork unitOfWork = null)
        {
            testEntityRepository = testEntityRepository ?? MockRepository.GenerateStub<ITestEntityRepository>();
            unitOfWork = unitOfWork ?? MockRepository.GenerateStub<IUnitOfWork>();
            
            var sut = new TestEntityService(testEntityRepository, unitOfWork);

            return sut;
        }
        #endregion
    }
}

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using HotDoge.Business.Interfaces;
using HotDoge.Business.Services;
using HotDoge.Entities;
using HotDoge.Persistence.Interfaces;
using HotDoge.Persistence.Repositories;
using HotDoge.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace HotDoge.Ioc
{
    public class UnityConfig
    {
        /// <summary>
        /// This method will be called in the Application_Start of our website, to register the IOC bindings.
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // Services
            container.RegisterType<ITestEntityService, TestEntityService>();
            
            // MyUserManagerService is instanciated once and registered as an instance because it shall be used throughout the whole app. Not sure this is the best way, though.
            container.RegisterInstance<IMyUserManagerService>(new MyUserManagerService(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DogeContext()))));
            // alternatively, use the line below and adapt constructor of MyUserManagerService to instanciate a new UserManager
            //container.RegisterType<IMyUserManagerService, MyUserManagerService>();
            


            // Repositories & Persistence
            container.RegisterType<ITestEntityRepository, TestEntityRepository>();
            container.RegisterType<IUnitOfWork, DogeUnitOfWork>();
            container.RegisterType<IDogeContext, DogeContext>(new PerResolveLifetimeManager()); // PerResolveLifetimeManager allows effective use of Unit Of Work (the context used in the repositories will be the same as the one from the uow) . Such pattern. uow!

           

            // sets MVC dependancy resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
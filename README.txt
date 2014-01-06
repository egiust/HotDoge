This is the HotDoge template project readme file.
egiust 06/01/2014

HotDoge is a bootstrap VS2013 solution for a basic website implementing useful features, in a maintainable and testable way. 
With HotDoge as the base for your code, you only need to concentrate on your features and forget boilerplate coding.
HotDoge website is based on the ASP.Net MVC5 template of visual studio 2013.

Important : 

- Before deploying your application, edit the file HotDoge.Persistence.Migrations.Configuration.cs to uncomment 'AddUserAndRole' lines (and change the passwords).
This will allow you to deploy the app with pre-existing users. 


Features : 

- MVC5 website
- Business and persistence layers
- Microsoft Identity framework
- Entity Framework 6 Code First
- Generic Repository
- Unit of Work
- Dependancy injection with Unity
- Error logging with ELMAH (log accessible for the canViewLogs role using /elmah url)
- Demo "TestEntity" entity with associated repository, business service, controller and view
- Demo unit tests of TestEntityRepository and TestEntityService
- Battle tested for Azure deployments


Lists of projects within the solution :

- HotDoge : MVC5 website
- HotDoge.Business : Holds business services
- HotDoge.Entities : Holds the entities definitions
- HotDoge.Ioc : Holds the Unity configuration
- HotDoge.Persistence :  DbContext and repositories
- HotDoge.Tests :  Unit tests of the repository and business service


Use it :

First, build the solution.

Change the name of the database to deploy to in HotDoge.Persistence.DogeContext.cs

Set up and deploy the database (migrations are already enabled on the persistence project)
	Add-Migration -Project "HotDoge.Persistence" Initial
	Update-Database -Project "HotDoge.Persistence"

Edit the file HotDoge.Persistence.Migrations.Configuration.cs to add some users and roles by uncommenting AddUserAndRole lines (please change default passwords).

Rebuild and check it out.

Access the TestEntity controller to play around with the data (you'll need to login as a user in the canAccessEntityController role)

To deploy on Windows Azure, follow this tutorial : http://www.windowsazure.com/en-us/develop/net/tutorials/web-site-with-sql-database/
If you ever need to empty the database without deleting it, there is a script in HotDoge.Persistence project "UsefulQueries" folder.



Doubts : 

HotDoge.Ioc.UnityConfig :
- MyUserManagerService is instanciated once and registered as an instance because it shall be used throughout the whole app. Not sure this is the best way, though.

HotDoge.Business.Services:
- Had to wrap UserManager<ApplicationUser> into a homemade IMyUserManagerService to allow for easy declaration of IOC in UnityConfig. Not sure this is the best approach.

HotDoge.Persistence.Repositories :
- Not really sure the GenericRepository is worth it

HotDoge.Persistence.DogeContext :
- DogeContext inherits from IdentityDbContext<ApplicationUser> and is thus tied to Identity framework. I did not find a way to have two contexts targeting the same database and deploying correctly with code first...

HotDoge.Tests :
- Unit tests of the repository 

HotDoge :
- TestEntity entity is directly used as a Model
- Many js files in the Scripts folder. Probably some of them can be removed
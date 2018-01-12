# FluentNHibernate.AspNet.Identity #
An ASP.NET Identity 2.1 provider for FluentNHibernate.  Supports MySQL, SQL Server, Oracle, DB/2, SQLite, Firebird, PostgreSQL, SQLAnyWhere, SQL Server Compact Edition

## Purpose ##

ASP.NET MVC 5 shipped with a new Identity system (in the Microsoft.AspNet.Identity.Core package) in order to support both local login and remote logins via OpenID/OAuth, but only ships with an
Entity Framework provider (Microsoft.AspNet.Identity.EntityFramework).

## Features ##
* Supports ASP.NET Identity 2.1
* Contains the same IdentityUser class used by the EntityFramework provider in the MVC 5 project template.
* Contains the same IdentityRole class used by the EntityFramework provider in the MVC 5 project template.
* Supports additional profile properties on your application's user model.
* Provides FluentNHibernateUserStore<TUser> implementation that implements the same interfaces as the EntityFramework version:

	- IUserStore<TUser>,
	- IUserLoginStore<TUser>,
	- IUserClaimStore<TUser>,
	- IUserRoleStore<TUser>,
	- IUserPasswordStore<TUser>,
	- IUserSecurityStampStore<TUser>,
	- IUserEmailStore<TUser>,
	- IUserLockoutStore<TUser, string>,
	- IUserTwoFactorStore<TUser, string>,
	- IUserPhoneNumberStore<TUser>

## Instructions ##

For more detailed instructions read this blog post : [ASP.NET Identity 2.1 implementation for MySQL](http://blog.developers.ba/asp-net-identity-2-1-for-MySQL/)

You don't need to execute any scripts.  The package will create required tables for you.

1. Create a new ASP.NET MVC 5 project, choosing the Individual User Accounts authentication type.
2. Remove the Entity Framework packages and replace with FluentNHibernate.AspNet.Identity.

	* Uninstall-Package Microsoft.AspNet.Identity.EntityFramework
	* Uninstall-Package EntityFramework
3. Install-Package FluentNHibernate.AspNet.Identity

    
4. In ~/Models/IdentityModels.cs:
    * Remove the namespaces: 
		* Microsoft.AspNet.Identity.EntityFramework
		* System.Data.Entity
5. Install NuGet Package called FluentNHibernate.AspNet.Identity
    * Add the namespace: FluentNHibernate.AspNet.Identity
	This way ApplicationUser will inherit from another IdentityUser which resides in FluentNHibernate.Asp.Net.Identity namespace
    * Remove the entire ApplicationDbContext class. You don't need that!
	
6. In ~/App_Start/Startup.Auth.cs

	* Remove app.CreatePerOwinContext(ApplicationDbContext.Create);

	
	
7. In ~/App_Start/IdentityConfig.cs
    
	* Remove the namespaces: 
	  	* Microsoft.AspNet.Identity.EntityFramework
		* System.Data.Entity
    * In method  public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
	replace ApplicationUserManager with another which accepts FluentNHibernateUserStore like this:

	* Find your particular RDBMS in the `ProviderTypeEnum` enumeration and provide a connection string.  You will need to install an [additional driver package](DriverPackage.md) for all RDBMS systems except SQL Server.
```C#
public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
{
     // var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
     var manager = new ApplicationUserManager(new FluentNHibernateUserStore<ApplicationUser>(ProviderTypeEnum.MySQL, "nameOrConnectionString"));
	 
```
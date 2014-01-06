using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HotDoge.Entities;
using HotDoge.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace HotDoge.Business.Interfaces
{
    /// <summary>
    /// Adapt the UserManager<ApplicationUser> into a simpler to use implementation of IMyUserManagerService
    /// Simplifies IOC declaration. Not sure this is the best approach, though, but it works.
    /// </summary>
    public interface IMyUserManagerService
    {
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        Task<IdentityResult> AddPasswordAsync(string userId, string password);
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
        void Dispose();
        ApplicationUser FindById(string userId);
        Task<ApplicationUser> FindAsync(UserLoginInfo login);        
        Task<ApplicationUser> FindAsync(string userName, string password);

        IList<UserLoginInfo> GetLogins(string userId);
        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo login);            
    }
}
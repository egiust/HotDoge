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
using HotDoge.Business.Interfaces;

namespace HotDoge.Business.Services
{
    /// <summary>
    /// Adapt the UserManager<ApplicationUser> into a simpler to use implementation of IMyUserManagerService
    /// Simplifies IOC declaration. Not sure this is the best approach, though, but it works.
    /// </summary>
    public class MyUserManagerService : IMyUserManagerService, IDisposable
    {

        private readonly UserManager<ApplicationUser> _userManager;

        //public MyUserManagerService()
        //{
        //    _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DogeContext()));
        //}
        public MyUserManagerService(UserManager<ApplicationUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            _userManager = userManager;
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            return _userManager.AddLoginAsync(userId, login);
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            return _userManager.AddPasswordAsync(userId, password);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return _userManager.CreateAsync(user);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            return _userManager.CreateIdentityAsync(user, authenticationType);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public ApplicationUser FindById(string userId)
        {
            return _userManager.FindById(userId);
        }
        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            return _userManager.FindAsync(login);
        }
        public Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return _userManager.FindAsync(userName, password);
        }

        public IList<UserLoginInfo> GetLogins(string userId)
        {
            return _userManager.GetLogins(userId);
        }
        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo login)
        {
            return _userManager.RemoveLoginAsync(userId, login);
        }
    }
}
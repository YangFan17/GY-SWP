using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using GYSWP.Authorization.Users;
using GYSWP.MultiTenancy;
using System.Collections.Generic;

namespace GYSWP
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class GYSWPAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected GYSWPAppServiceBase()
        {
            LocalizationSourceName = GYSWPConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected async Task<IList<string>> GetUserRolesAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            var roles = await UserManager.GetRolesAsync(currentUser);
            return roles;
        }

        protected async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            var user = await UserManager.GetUsersInRoleAsync(roleName);
            return user;
        }
    }
}

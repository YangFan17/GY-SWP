using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace GYSWP.Controllers
{
    public abstract class GYSWPControllerBase: AbpController
    {
        protected GYSWPControllerBase()
        {
            LocalizationSourceName = GYSWPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

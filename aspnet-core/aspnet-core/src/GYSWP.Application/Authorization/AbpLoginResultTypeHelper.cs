using System;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.UI;

namespace GYSWP.Authorization
{
    public class AbpLoginResultTypeHelper : AbpServiceBase, ITransientDependency
    {
        public AbpLoginResultTypeHelper()
        {
            LocalizationSourceName = GYSWPConsts.LocalizationSourceName;
        }

        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException("登录失败!", "用户名或密码无效");
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException("登录失败!", L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException("登录失败!", L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException("登录失败!", L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException("登录失败!", L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException("登录失败!", "当前账号被锁定");
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException("登录失败!");
            }
        }

        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return "用户名或密码无效";
                case AbpLoginResultType.InvalidTenancyName:
                    return L("ThereIsNoTenantDefinedWithName{0}", tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L("TenantIsNotActive", tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L("UserEmailIsNotConfirmedAndCanNotLogin");
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return "登录失败!";
            }
        }
    }
}

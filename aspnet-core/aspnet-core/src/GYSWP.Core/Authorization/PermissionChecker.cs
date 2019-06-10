using Abp.Authorization;
using GYSWP.Authorization.Roles;
using GYSWP.Authorization.Users;

namespace GYSWP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

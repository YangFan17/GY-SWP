

using System.Linq;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using GYSWP.Authorization;

namespace GYSWP.LC_WarningReports.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="LC_WarningReportPermissions" /> for all permission names. LC_WarningReport
    ///</summary>
    public class LC_WarningReportAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

		public LC_WarningReportAuthorizationProvider()
		{

		}

        public LC_WarningReportAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public LC_WarningReportAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			// 在这里配置了LC_WarningReport 的权限。
			var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

			var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

			var entityPermission = administration.CreateChildPermission(LC_WarningReportPermissions.Node , L("LC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.Query, L("QueryLC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.Create, L("CreateLC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.Edit, L("EditLC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.Delete, L("DeleteLC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.BatchDelete, L("BatchDeleteLC_WarningReport"));
			entityPermission.CreateChildPermission(LC_WarningReportPermissions.ExportExcel, L("ExportExcelLC_WarningReport"));


		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, GYSWPConsts.LocalizationSourceName);
		}
    }
}
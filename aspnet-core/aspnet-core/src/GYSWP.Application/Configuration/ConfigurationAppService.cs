using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GYSWP.Configuration.Dto;

namespace GYSWP.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GYSWPAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

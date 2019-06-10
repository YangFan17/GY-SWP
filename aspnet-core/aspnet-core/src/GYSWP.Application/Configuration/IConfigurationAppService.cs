using System.Threading.Tasks;
using GYSWP.Configuration.Dto;

namespace GYSWP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using GYSWP.Authorization.Accounts.Dto;

namespace GYSWP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

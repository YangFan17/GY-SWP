using System.Threading.Tasks;
using Abp.Application.Services;
using GYSWP.Sessions.Dto;

namespace GYSWP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

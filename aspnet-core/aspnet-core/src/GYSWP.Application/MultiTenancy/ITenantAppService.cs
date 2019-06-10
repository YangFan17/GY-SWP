using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GYSWP.MultiTenancy.Dto;

namespace GYSWP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


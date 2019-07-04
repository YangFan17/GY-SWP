using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GYSWP.Dtos;
using GYSWP.Roles.Dto;
using GYSWP.Users.Dto;

namespace GYSWP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        /// <summary>
        /// ͬ�������û�
        /// </summary>
        /// <returns></returns>
        Task<APIResultDto> SynchroDingUserAsync();
    }
}

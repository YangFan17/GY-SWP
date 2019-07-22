using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;

using GYSWP.Authorization;
using GYSWP.Authorization.Accounts;
using GYSWP.Authorization.Roles;
using GYSWP.Authorization.Users;
using GYSWP.DingDing;
using GYSWP.DingDing.Dtos;
using GYSWP.Dtos;
using GYSWP.Employees;
using GYSWP.Organizations.Dtos;
using GYSWP.Roles.Dto;
using GYSWP.Users.Dto;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.International.Converters.PinYinConverter;

namespace GYSWP.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IDingDingAppService _dingDingAppService;
        private readonly IEmployeeAppService _employeeAppService;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            IDingDingAppService dingDingAppService,
            IEmployeeAppService employeeAppService,
            LogInManager logInManager)
            : base(repository)
        {
            _dingDingAppService = dingDingAppService;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _employeeAppService = employeeAppService;
        }
        public override async Task<PagedResultDto<UserDto>> GetAll(PagedUserResultRequestDto input)
        {
            var query = base.Repository.GetAll().WhereIf(!string.IsNullOrEmpty(input.Keyword),v => v.Name.Contains(input.Keyword));
            var count = await query.CountAsync();
            var entityList = await query
                    .AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<UserDto>>();
            return new PagedResultDto<UserDto>(count, entityListDtos);
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// 同步钉钉用户
        /// </summary>
        /// <returns></returns>
        public async Task<APIResultDto> SynchroDingUserAsync()
        {
            //DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            //string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            //var depts = Senparc.CO2NET.HttpUtility.Get.GetJson<DingDepartmentDto>(string.Format("https://oapi.dingtalk.com/department/list?access_token={0}", accessToken));
            //var entityByDD = depts.department.Select(o => new OrganizationListDto()
            //{
            //    Id = o.id
            //}).ToList();

            //foreach (var item in entityByDD)
            //{
            //    if (item.Id != 1)
            //        await CreateOrUpdateUsers(item.Id, accessToken);
            //}
            //await CurrentUnitOfWork.SaveChangesAsync();
            await CreateUsersByEmployeeListAsync();
            return new APIResultDto() { Code = 0, Msg = "同步组织架构成功" };
        }

        /// <summary>
        /// 基于员工表同步账号
        /// </summary>
        /// <returns></returns>
        private async Task<UserDto> CreateUsersByEmployeeListAsync()
        {
            string[] relesName = await _roleRepository.GetAll().Where(aa => aa.DisplayName == "员工").Select(aa => aa.Name).ToArrayAsync();
            var userList = await _employeeAppService.GetAllEmployeeListAsync();
            var entityByDD = userList.Select(e => new SynchroDingUser()
            {
                UserName = GetPinyin(e.Name) + e.Mobile.Substring(7, 4),
                IsActive = true,
                EmailAddress = "GYSWP" + GetPinyin(e.Name) + e.Mobile.Substring(7, 4) + "@gy.com",
                Name = e.Name,
                Surname = e.Name,
                Password = "123qwe",
                EmployeeId = e.Id,
                EmployeeName = e.Name,
                UnionId = e.Unionid,
                RoleNames = relesName,
                PhoneNumber = e.Mobile
            }).ToList();
            foreach (var item in entityByDD)
            {
                CheckCreatePermission();
                var users = ObjectMapper.Map<User>(item);
                users.TenantId = AbpSession.TenantId;
                users.IsEmailConfirmed = true;
                await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                CheckErrors(await _userManager.CreateAsync(users, item.Password));
                if (item.RoleNames != null)
                {
                    CheckErrors(await _userManager.SetRoles(users, item.RoleNames));
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return null;
        }

        private async Task<UserDto> CreateOrUpdateUsers(long deptId, string accessToken)
        {
            string[] relesName = await _roleRepository.GetAll().Where(aa => aa.DisplayName == "员工").Select(aa => aa.Name).ToArrayAsync();
            var url = string.Format("https://oapi.dingtalk.com/user/list?access_token={0}&department_id={1}", accessToken, deptId);
            var user = Senparc.CO2NET.HttpUtility.Get.GetJson<DingUserListDto>(url);
            var entityByDD = user.userlist.Select(e => new SynchroDingUser()
            {
                UserName = GetPinyin(e.name) + e.mobile.Substring(7, 4),
                IsActive = true,
                //IsEmailConfirmed = true,
                //IsLockoutEnabled = true,
                EmailAddress = "GYSWP" + GetPinyin(e.name) + e.mobile.Substring(7, 4) + "@gy.com",
                Name = e.name,
                Surname = e.name.Substring(0, 1),
                Password = "123qwe",
                EmployeeId = e.userid,
                EmployeeName = e.name,
                UnionId = e.unionid,
                RoleNames = relesName,
                PhoneNumber = e.mobile
            }).ToList();
            //int emailCode = 001;
            foreach (var item in entityByDD)
            {
                //emailCode += 1;
                //item.EmailAddress = "GYSWP" + emailCode + "@gy.com";
                CheckCreatePermission();

                var users = ObjectMapper.Map<User>(item);

                users.TenantId = AbpSession.TenantId;
                users.IsEmailConfirmed = true;

                await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                await _userManager.CreateAsync(users, item.Password);
                CheckErrors(await _userManager.CreateAsync(users, item.Password));

                if (item.RoleNames != null)
                {
                    CheckErrors(await _userManager.SetRoles(users, item.RoleNames));
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return null;
        }

        /// <summary> 
        /// 汉字转化为拼音
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>全拼</returns> 
        public static string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r.ToLower();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using GYSWP.Authentication.External;
using GYSWP.Authentication.JwtBearer;
using GYSWP.Authorization;
using GYSWP.Authorization.Users;
using GYSWP.Models.TokenAuth;
using GYSWP.MultiTenancy;
using GYSWP.Employees.Dtos;
using GYSWP.DingDing;
using GYSWP.Employees;
using GYSWP.DingDing.Dtos;
using Senparc.CO2NET.Helpers;
using System.IO;
using System.Text;
using Senparc.CO2NET.HttpUtility;

namespace GYSWP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : GYSWPControllerBase
    {
        private string APPID = "dingoanherbetgt7ld5rrh";
        private string REDIRECT_URI = "http://localhost:21021";
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        //IDingDingAppService _dingDingAppService;
        //IEmployeeAppService _employeeAppService;

        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager
            //, IDingDingAppService dingDingAppService
            //, IEmployeeAppService employeeAppService
            )
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            //_dingDingAppService = dingDingAppService;
            //_employeeAppService = employeeAppService;
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                //"123qwe",
                GetTenancyNameOrNull()
            );

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id
            };
        }

        //[HttpGet]
        //public object AuthenticateByScanCodeAsync123(string loginTmpCode)
        //{
        //   var x= Redirect(string.Format("https://oapi.dingtalk.com/connect/oauth2/sns_authorize?appid={0}&response_type=code&scope=snsapi_login&state=STATE&redirect_uri={1}&loginTmpCode={0}", "dingoanherbetgt7ld5rrh", string.Format("http://localhost:21021/api/TokenAuth/AuthenticateByScanCodeAsync"), loginTmpCode));
        //    return x;
        //}


        ///// <summary>
        ///// 扫码登录验证
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ScanUserInfo AuthenticateByScanCodeAsync(string code,string state)
        //{
        //    string appSecret = "--HvFZSQx765LkFskrrKhELYQZdSqpxUgDEYktz60D860O45QTNCRYosZ-SXsB3E";
        //    string accessToken = _dingDingAppService.GetAccessTokenByAppId(APPID, appSecret);
        //    var url = string.Format("https://oapi.dingtalk.com/sns/getuserinfo_bycode?access_token={0}", accessToken);
        //    ScanLogin dto = new ScanLogin();
        //    dto.tmp_auth_code = code;
        //    var jsonString = SerializerHelper.GetJsonString(dto, null);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var bytes = Encoding.UTF8.GetBytes(jsonString);
        //        ms.Write(bytes, 0, bytes.Length);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        var obj = Post.PostGetJson<ScanLoginInfo>(url, null, ms);
        //        if (obj.errcode == "0")
        //        {
        //            var user = _employeeAppService.GetEmployeeByUnionIdAsync(obj.user_info.unionId).Result;
        //            //return Json(user);
        //            //AuthenticateModel model = new AuthenticateModel();
        //            user.Password = "123qwe";
        //            //user.UserNameOrEmailAddress = user.UserName;
        //            //var result = await Authenticate(model);
        //            return user;
        //            //return Redirect("http://localhost:4200/account/login?name={0}&pwd={1}",user.UserName,user.Password);
        //            //return model;
        //        }
        //        else
        //        {
        //            //return Redirect("http://localhost:4200/gyswp/#/app/home");
        //            return null;
        //        }
        //    }
        //}

        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        // Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}

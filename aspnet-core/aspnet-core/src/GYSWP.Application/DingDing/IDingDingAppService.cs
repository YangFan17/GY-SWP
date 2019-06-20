using Abp.Application.Services;
using GYSWP.DingDing.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing
{
    public interface IDingDingAppService : IApplicationService
    {
        string GetAccessToken(string appkey, string appsecret);
        string GetAccessTokenByAppId(string appId, string appsecret);
        string GetUserId(string accessToken, string code);
        DingDingAppConfig GetDingDingConfigByApp(DingDingAppEnum app);
    }
}

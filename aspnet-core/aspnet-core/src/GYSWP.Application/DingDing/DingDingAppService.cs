using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using GYSWP.DingDing.Dtos;
using GYSWP.GYEnums;
using GYSWP.SystemDatas;
using Senparc.CO2NET.HttpUtility;
using System.Collections.Generic;
using System.Linq;

namespace GYSWP.DingDing
{
    [AbpAllowAnonymous]
    [Audited]
    public class DingDingAppService : GYSWPAppServiceBase, IDingDingAppService
    {
        private readonly IRepository<SystemData> _systemDataRepository;

        public DingDingAppService(IRepository<SystemData> systemDataRepository)
        {
            _systemDataRepository = systemDataRepository;
        }

        /// <summary>
        /// 获取addess token
        /// </summary>       
        public string GetAccessToken(string appkey, string appsecret)
        {
            AccessTokenDto accessTokenDto = Get.GetJson<AccessTokenDto>(string.Format("https://oapi.dingtalk.com/gettoken?appkey={0}&appsecret={1}", appkey, appsecret));
            Logger.InfoFormat("AccessToken response errmsg:{0} body:{1}", accessTokenDto.errmsg, accessTokenDto.access_token);
            return accessTokenDto.access_token;
        }
        public string GetAccessTokenByAppId(string appId, string appsecret)
        {
            AccessTokenDto accessTokenDto = Get.GetJson<AccessTokenDto>(string.Format("https://oapi.dingtalk.com/sns/gettoken?appid={0}&appsecret={1}", appId, appsecret));
            Logger.InfoFormat("AccessToken response errmsg:{0} body:{1}", accessTokenDto.errmsg, accessTokenDto.access_token);
            return accessTokenDto.access_token;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        public string GetUserId(string accessToken, string code)
        {
            DingUserInfoDto user = Get.GetJson<DingUserInfoDto>(string.Format("https://oapi.dingtalk.com/user/getuserinfo?access_token={0}&code={1}", accessToken, code));
            Logger.InfoFormat("Userid response errmsg:{0} body:{1}", user.errmsg, user.userid);
            return user.userid;
        }


        /// <summary>
        /// 获取钉钉AccessToken 根据App
        /// </summary>
        public string GetAccessTokenByApp(DingDingAppEnum app)
        {
            var config = GetDingDingConfigByApp(app);
            return GetAccessToken(config.Appkey, config.Appsecret);
        }

        /// <summary>
        /// 获取钉钉配置根据 应用App
        /// </summary>
        public DingDingAppConfig GetDingDingConfigByApp(DingDingAppEnum app)
        {
            DingDingAppConfig config = new DingDingAppConfig();
            var configList = new List<SystemData>();
            switch (app)
            {
                case DingDingAppEnum.标准化工作平台:
                    {
                        configList = _systemDataRepository.GetAll()
                                    .Where(s => s.ModelId == ConfigModel.钉钉配置)
                                    .Where(s => s.Type == ConfigType.钉钉配置 || s.Type == ConfigType.标准化工作平台)
                                    .ToList();
                    }
                    break;
                case DingDingAppEnum.设备管理:
                    {
                        configList = _systemDataRepository.GetAll()
                                    .Where(s => s.ModelId == ConfigModel.钉钉配置)
                                    .Where(s => s.Type == ConfigType.钉钉配置 || s.Type == ConfigType.设备管理)
                                    .ToList();
                    }
                    break;
            }
            foreach (var item in configList)
            {
                if (item.Code == DingDingConfigCode.CorpId)
                {
                    config.CorpId = item.Desc;
                }
                else if (item.Code == DingDingConfigCode.Appkey)
                {
                    config.Appkey = item.Desc;
                }
                else if (item.Code == DingDingConfigCode.Appsecret)
                {
                    config.Appsecret = item.Desc;
                }
                else if (item.Code == DingDingConfigCode.AgentID)
                {
                    if (int.TryParse(item.Desc, out int outAgenId))
                    {
                        config.AgentID = outAgenId;
                    }
                }
            }
            return config;
        }

    }
}

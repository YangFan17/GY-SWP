using Abp.AspNetCore.Mvc.Controllers;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using GYSWP.ApplyInfos;
using GYSWP.Controllers;
using GYSWP.DingDing;
using GYSWP.DingDing.Dtos;
using GYSWP.DingDingApproval.Dtos;
using GYSWP.DingDingApproval.Dtos.ApprovalCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.Web.Host.Controllers
{
    public class DingTalkController : GYSWPControllerBase
    {
        //private readonly IDingTalkManager _dingTalkManager;
        //private readonly IReimburseManager _reimburseManager;
        IApplyInfoAppService _applyInfoAppService;
        IDingDingAppService _dingDingAppService;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public DingTalkController(IDingDingAppService dingDingAppService
            , IApplyInfoAppService applyInfoAppService
        )
        {
            _dingDingAppService = dingDingAppService;
            _applyInfoAppService = applyInfoAppService;
        }

        /// <summary>
        /// 创建审批事件
        /// </summary>
        /// <returns></returns>
        public object CreateApprovalCallbackEventAsync()
        {
            //string accessToken = "517c709019d93056b8e01a5a1519d233";
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var url = string.Format("https://oapi.dingtalk.com/call_back/register_call_back?access_token={0}", accessToken);
            CallBackRequest request = new CallBackRequest();
            request.call_back_tag = new List<string>();
            request.call_back_tag.Add("bpms_instance_change");
            //request.url = "http://yangfan.vaiwan.com/DingTalk/ApprovalCallbackAsync";
            request.url = "http://gy.intcov.com/DingTalk/ApprovalCallbackAsync";
            request.aes_key = "99skhqweass5232345IUJKWEDL5251054DSFdsuhfW9";
            request.token = "12345";
            var jsonString = SerializerHelper.GetJsonString(request, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = Post.PostGetJson<object>(url, null, ms);
                return obj;
            };
        }

        /// <summary>
        /// 审批事件参数回调
        /// </summary>
        /// <param name="approvalCallbackTestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task ApprovalCallbackAsync(string signature, string timestamp, string nonce)
        {
            string token = "12345";
            string suitekey = "ding6f6f3ad4521c207335c2f4657eb6378f";
            string aes_key = "99skhqweass5232345IUJKWEDL5251054DSFdsuhfW9";
            //post数据包数据中的加密数据
            var encryptStr = GetPostParam(Request.GetRequestMemoryStream());

            DingTalkCrypt dingTalk = new DingTalkCrypt(token, aes_key, suitekey);
            string sEchoStr = "";
            int ret = dingTalk.VerifyURL(signature, timestamp, nonce, encryptStr, ref sEchoStr);

            //解密接受信息，进行事件处理
            string plainText = "";
            ret = dingTalk.DecryptMsg(signature, timestamp, nonce, encryptStr, ref plainText);

            Hashtable tb = (Hashtable)JsonConvert.DeserializeObject(plainText, typeof(Hashtable));
            string eventType = tb["EventType"].ToString();
            string res = "success";
            switch (eventType)
            {
                case "bpms_instance_change"://审批实例开始与结束，执行代码
                    #region 审批实例开始与结束，执行代码
                    string type = tb["type"].ToString();
                    string title = tb["title"].ToString();
                    if (type == "start")
                    {
                        return;
                    }
                    string result = tb["result"].ToString();
                    string processInstanceId = tb["processInstanceId"].ToString();

                    if (type == "finish")//审批实例结束
                    {
                        if (title.Contains("制修订发起流程测试"))
                        {
                            await _applyInfoAppService.UpdateApplyInfoByPIIdAsync(processInstanceId, result);
                            return;
                        }
                        else if (title.Contains("修订审核流程测试"))
                        {
                            await _applyInfoAppService.UpdateDocClauseByPIIdAsync(processInstanceId, result);
                            return;
                        }
                        else if (title.Contains("制定审批流程测试"))
                        {
                            await _applyInfoAppService.CreateDraDocByPIIdAsync(processInstanceId, result);
                            return;
                        }
                        else //其他
                        {

                        }
                    }
                    else//审批实例终止
                    {

                    }
                    #endregion
                    break;
                default:
                    break;
            }

            timestamp = GetTimeStamp().ToString();
            string encrypt = "";
            string signature2 = "";
            dingTalk = new DingTalkCrypt(token, aes_key, suitekey);
            ret = dingTalk.EncryptMsg(res, timestamp, nonce, ref encrypt, ref signature2);
            Hashtable jsonMap = new Hashtable
                {
                    {"msg_signature", signature2},
                    {"encrypt", encrypt},
                    {"timeStamp", timestamp},
                    {"nonce", nonce}
                };
            var bbs = SerializerHelper.GetJsonString(jsonMap);
            await HttpContext.Response.WriteAsync(bbs);

        }

        private string GetPostParam(Stream stream)
        {
            Stream sm = stream;//获取post正文
            int len = (int)sm.Length;//post数据长度
            byte[] inputByts = new byte[len];//字节数据,用于存储post数据
            sm.Read(inputByts, 0, len);//将post数据写入byte数组中
            sm.Close();//关闭IO流

            //**********下面是把字节数组类型转换成字符串**********
            string data = Encoding.UTF8.GetString(inputByts);//转为String
            data = data.Replace("{\"encrypt\":\"", "").Replace("\"}", "");
            return data;
        }

        public static double GetTimeStamp()
        {
            DateTime dt1 = Convert.ToDateTime("1970-01-01 00:00:00");
            TimeSpan ts = DateTime.Now - dt1;
            return Math.Ceiling(ts.TotalSeconds);
        }

        /// <summary>
        /// 查询已创建的回调事件
        /// </summary>
        /// <returns></returns>
        public object GetCallBack()
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var response = Get.GetJson<object>(string.Format("https://oapi.dingtalk.com/call_back/get_call_back?access_token={0}", accessToken));
            return response;
        }

        /// <summary>
        /// 更新已创建的回调事件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<OapiCallBackUpdateCallBackResponse> UpdateCallBackAsync()
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            //var url = string.Format("https://oapi.dingtalk.com/call_back/update_call_back?access_token={0}", accessToken);
            DefaultDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/call_back/update_call_back");
            OapiCallBackUpdateCallBackRequest request = new OapiCallBackUpdateCallBackRequest();
            //request.Url = "http://pm.hechuangcd.com/DingTalk/ApprovalCallbackAsync";
            request.Url = "http://gy.intcov.com/DingTalk/ApprovalCallbackAsync";
            request.AesKey = "45skhqweass5232345IUJKWEDL5251054DSFdsuhfW1";
            request.Token = "123";
            List<string> items = new List<string>();
            items.Add("bpms_instance_change");
            request.CallBackTag = items;
            var rq = SerializerHelper.GetJsonString(request, null);
            OapiCallBackUpdateCallBackResponse response = client.Execute(request, accessToken);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    var bytes = Encoding.UTF8.GetBytes(rq);
            //    ms.Write(bytes, 0, bytes.Length);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    response = Post.PostGetJson<OapiCallBackUpdateCallBackResponse>(url, null, ms);
            //};
            return response;
            //OapiCallBackUpdateCallBackResponse response = client.execute(request, accessToken);
        }

        public object RemoveCallBack()
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var response = Get.GetJson<object>(string.Format("https://oapi.dingtalk.com/call_back/delete_call_back?access_token={0}", accessToken));
            return response;
        }
    }
}
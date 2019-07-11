using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using GYSWP.Controllers;
using GYSWP.DingDing.Dtos;
using Senparc.CO2NET.Helpers;
using System.IO;
using System.Text;
using Senparc.CO2NET.HttpUtility;
using GYSWP.DingDing;
using GYSWP.Employees;

namespace GYSWP.Web.Host.Controllers
{
    public class HomeController : GYSWPControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;
        //private string APPID = "dingoa6hra7f79otenwx0y";
        private string APPID = "dingoanherbetgt7ld5rrh";
        private string REDIRECT_URI = "http://gy.intcov.com";
        //private string REDIRECT_URI = "http://localhost:21021";
        //private string REDIRECT_URI = "http://yangfan.vaiwan.com";

        IDingDingAppService _dingDingAppService;
        IEmployeeAppService _employeeAppService;


        public HomeController(INotificationPublisher notificationPublisher
            , IDingDingAppService dingDingAppService
            , IEmployeeAppService employeeAppService)
        {
            _notificationPublisher = notificationPublisher;
            _dingDingAppService = dingDingAppService;
            _employeeAppService = employeeAppService;
        }

        public IActionResult Index(string code)
        {
            return Redirect("/gyswp/index.html");
            //return Redirect("/swagger");
            #region ×¢ÊÍ
            //if (!string.IsNullOrEmpty(code))
            //{
            //    //string accessToken = "b759f2aae1813d679fa728b731758160";
            //    //var userId = _dingDingAppService.GetUserId(accessToken, code);
            //    //string appId = "dingoalplojp7nlay8p1x5";
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
            //            return Json(user);
            //            //return RedirectToAction("Authenticate", "TokenAuth",user);
            //        }
            //        else
            //        {
            //            return Redirect(string.Format("https://oapi.dingtalk.com/connect/qrconnect?appid={0}&response_type=code&scope=snsapi_login&state=STATE&redirect_uri={1}", APPID, REDIRECT_URI));
            //        }
            //    };
            //}
            //return Redirect("/swagger");
            ////return Redirect(string.Format("https://oapi.dingtalk.com/connect/qrconnect?appid={0}&response_type=code&scope=snsapi_login&state=STATE&redirect_uri={1}", APPID, REDIRECT_URI));
            #endregion
        }

        public IActionResult AuthenticateByScanCodeAsync(string code,  string state)
        {
            string appSecret = "--HvFZSQx765LkFskrrKhELYQZdSqpxUgDEYktz60D860O45QTNCRYosZ-SXsB3E";
            string accessToken = _dingDingAppService.GetAccessTokenByAppId(APPID, appSecret);
            var url = string.Format("https://oapi.dingtalk.com/sns/getuserinfo_bycode?access_token={0}", accessToken);
            ScanLogin dto = new ScanLogin();
            dto.tmp_auth_code = code;
            var jsonString = SerializerHelper.GetJsonString(dto, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = Post.PostGetJson<ScanLoginInfo>(url, null, ms);
                if (obj.errcode == "0")
                {
                    var user = _employeeAppService.GetEmployeeByUnionIdAsync(obj.user_info.unionId).Result;
                    //user.Password = "123qwe";
                    return Redirect(string.Format("http://gy.intcov.com/account/login;name={0};flag={1}", user.UserName,"SUCCESS"));
                }
                else
                {
                    //return Redirect(string.Format("http://localhost:4200/account/login;name={0};flag={1}", user.UserName, "SUCCESS"));
                    return null;
                }
            }
        }

        public void ScanLoginRedirect(string loginTmpCode)
        {
            if (!string.IsNullOrEmpty(loginTmpCode))
            {
                string appSecret = "--HvFZSQx765LkFskrrKhELYQZdSqpxUgDEYktz60D860O45QTNCRYosZ-SXsB3E";
                string accessToken = _dingDingAppService.GetAccessTokenByAppId(APPID, appSecret);
                var url = string.Format("https://oapi.dingtalk.com/connect/oauth2/sns_authorize?appid={0}&response_type=code&scope=snsapi_login&state=STATE&redirect_uri={1}&loginTmpCode={2}", APPID, REDIRECT_URI, loginTmpCode);
            }
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }
    }
}

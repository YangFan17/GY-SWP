using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Auditing;
using Abp.Authorization;
using GYSWP.Configuration;
using GYSWP.Documents;
using GYSWP.Documents.Dtos;
using GYSWP.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYSWP.Web.Host.Controllers
{
    public class GYSWPFileController : AbpController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDocumentAppService _documentAppService;

        public GYSWPFileController(IHostingEnvironment hostingEnvironment
            , IDocumentAppService documentAppService
          )
        {
            this._hostingEnvironment = hostingEnvironment;
            _documentAppService = documentAppService;
        }

        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        [AbpAllowAnonymous]
        //[Audited]
        public async Task<JsonResult> DocFilesPostsAsync(IFormFile[] file)
        {
            var date = Request;
            var files = Request.Form.Files;
            //long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var filePath = string.Empty;
            var returnUrl = string.Empty;
            var fileName = string.Empty;
            long fileSize = 0;
            string fileExt = string.Empty;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    fileName = formFile.FileName.Substring(0, formFile.FileName.IndexOf('.'));
                    fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    fileSize = formFile.Length; //获得文件大小，以字节为单位
                    var uid = Guid.NewGuid().ToString();
                    string newFileName = uid + fileExt; //随机生成新的文件名
                    var fileDire = webRootPath + "/docfiles/";
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }
                    filePath = fileDire + newFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    returnUrl = "/docfiles/" + newFileName;
                }
            }
            var apiResult = new APIResultDto();
            apiResult.Code = 0;
            apiResult.Msg = "上传文件成功";
            apiResult.Data = new { name = fileName, size = fileSize, ext = fileExt, url = returnUrl };
            return Json(apiResult);
        }

        /// <summary>
        /// 批量导入标准文档txt
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        [AbpAllowAnonymous]
        //[Audited]
        public async Task<JsonResult> DocFilesTxTPostsAsync(IFormFile[] file, int categoryId)
        {
            var files = Request.Form.Files;
            string webRootPath = _hostingEnvironment.WebRootPath;
            var filePath = string.Empty;
            var fileName = string.Empty;
            string fileExt = string.Empty;
            //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //string time = Convert.ToInt64(ts.TotalSeconds).ToString();
            //var fileDire = webRootPath + "/txtUpload/" + Guid.NewGuid();
            var fileDire = webRootPath + "/txtUpload/" + Guid.NewGuid();
            if (!Directory.Exists(fileDire))
            {
                Directory.CreateDirectory(fileDire);
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //fileName = formFile.FileName.Substring(0, formFile.FileName.IndexOf('.'));
                    var originTxt = formFile.FileName.Split('/')[1];
                    fileName = originTxt.Substring(0, originTxt.IndexOf('.'));
                    fileExt = Path.GetExtension(originTxt); //文件扩展名，不含“.”
                    filePath = fileDire +'/'+ fileName+ fileExt;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            DocumentReadInput input = new DocumentReadInput();
            input.CategoryId = categoryId;
            input.Path = fileDire;
            await _documentAppService.DocumentReadAsync(input);
            var apiResult = new APIResultDto();
            apiResult.Code = 0;
            apiResult.Msg = "上传文件成功";
            return Json(apiResult);
        }
    }
}
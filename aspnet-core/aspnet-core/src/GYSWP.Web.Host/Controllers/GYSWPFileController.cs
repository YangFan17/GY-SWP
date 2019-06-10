using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GYSWP.Web.Host.Controllers
{
    public class GYSWPFileController : AbpController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GYSWPFileController(IHostingEnvironment hostingEnvironment
          )
        {
            this._hostingEnvironment = hostingEnvironment;
        }
    }
}
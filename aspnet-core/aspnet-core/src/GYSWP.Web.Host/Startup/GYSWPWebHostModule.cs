using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GYSWP.Configuration;

namespace GYSWP.Web.Host.Startup
{
    [DependsOn(
       typeof(GYSWPWebCoreModule))]
    public class GYSWPWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GYSWPWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GYSWPWebHostModule).GetAssembly());
        }
    }
}

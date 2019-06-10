using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GYSWP.Authorization;

namespace GYSWP
{
    [DependsOn(
        typeof(GYSWPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class GYSWPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GYSWPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(GYSWPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

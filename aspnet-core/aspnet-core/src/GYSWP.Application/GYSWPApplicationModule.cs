using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Quartz;
using Abp.Quartz.Configuration;
using Abp.Reflection.Extensions;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using GYSWP.Authorization;
using GYSWP.AutoBackGroundJobs;
using Quartz;

namespace GYSWP
{
    [DependsOn(
        typeof(GYSWPCoreModule), 
        typeof(AbpAutoMapperModule),
        typeof(AbpQuartzModule))]
    public class GYSWPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GYSWPAuthorizationProvider>();
            Configuration.Modules.AbpQuartz().Scheduler.JobFactory = new AbpQuartzJobFactory(IocManager);
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

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IJobListener, AbpQuartzJobListener>();

            Configuration.Modules.AbpQuartz().Scheduler.ListenerManager.AddJobListener(IocManager.Resolve<IJobListener>());

            //后期解除注释
            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().Add(IocManager.Resolve<IQuartzScheduleJobManager>());
                ConfigureQuartzScheduleJobs();
            }
        }

        /// <summary>
        /// 调度jobs
        /// </summary>
        private void ConfigureQuartzScheduleJobs()
        {
            var jobManager = IocManager.Resolve<IQuartzScheduleJobManager>();
            //var startTime = DateTime.Today.AddHours(2);
            //if (startTime < DateTime.Now)
            //{
            //    startTime.AddDays(1);
            //}
            AsyncHelper.RunSync(() => jobManager.ScheduleAsync<ExamineStatusJob>(job =>
            {
                job.WithIdentity("AutoUpdateStatusJob", "TaskGroup").WithDescription("A job to update task status.");
            },
            trigger =>
            {
                trigger//.StartAt(new DateTimeOffset(startTime))
                .StartNow()//一旦加入scheduler，立即生效
                .WithCronSchedule("0 0 2 * * ?")//每天凌晨2点执行
                .Build();
            }));
        }
    }
}

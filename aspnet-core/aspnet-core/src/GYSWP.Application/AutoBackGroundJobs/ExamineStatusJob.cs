using Abp.Dependency;
using Abp.Quartz;
using GYSWP.CriterionExamines;
using GYSWP.Indicators;
using Quartz;
using System.Threading.Tasks;

namespace GYSWP.AutoBackGroundJobs
{
    public class ExamineStatusJob : JobBase, ITransientDependency
    {
        private readonly ICriterionExamineAppService _criterionExamineAppService;
        private readonly IIndicatorAppService _indicatorAppService;

        public ExamineStatusJob(ICriterionExamineAppService criterionExamineAppService
            , IIndicatorAppService indicatorAppService)
        {
            _criterionExamineAppService = criterionExamineAppService;
            _indicatorAppService = indicatorAppService;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            //执行标准检查状态更新脚本
            await _criterionExamineAppService.AutoUpdateCriterionStatusAsync();
            //执行指标考核状态更新脚本
            await _indicatorAppService.AutoUpdateIndicatorStatusAsync();
        }
    }
}

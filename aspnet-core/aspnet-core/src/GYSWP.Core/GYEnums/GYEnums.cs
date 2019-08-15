using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.GYEnums
{
    public enum ConfigType
    {
        钉钉配置 = 1,
        标准化工作平台 = 2,
        设备管理 = 3
    }
    public enum ConfigModel
    {
        标准化工作平台 = 1,
        物流中心 = 2,
        钉钉配置 = 3
    }

    public enum ApplyStatus
    {
        待审批 = 1,
        审批通过 = 2,
        审批拒绝 = 3
    }

    public enum ApplyType
    {
        制修订申请 = 1,
        制修订审批 = 2,
        合理化建议 = 3
    }

    public enum OperateType
    {
        制定标准 = 1,
        修订标准 = 2,
        废止标准 = 3,
        合理化建议 = 4
    }

    public enum RevisionStatus
    {
        待审核 = 1,
        审核通过 = 2,
        审核拒绝 = 3,
        前置拒绝 = 4,
        等待提交 = 5
    }
    public enum RevisionType
    {
        新增 = 1,
        修订 = 2,
        删除 = 3,
        标准制定 = 4
    }
    public enum CriterionExamineType
    {
        内部考核 = 1,
        外部考核 = 2
    }
    public enum ExamineStatus
    {
        未检查 = 1,
        合格 = 2,
        不合格 = 3
    }

    public enum ResultStatus
    {
        未开始 = 1,
        已完成 = 2
    }

    public enum FeedType
    {
        标准考核 = 1,
        考核指标 = 2
    }

    public enum FactorType
    {
        人 = 1,
        机 = 2,
        料 = 3,
        发 = 4,
        环 = 5
    }

    public enum AttachmentType
    {
        标准附件 = 1,
        条款附件 = 2,
        考核附件 = 3
    }

    public enum IndicatorStatus
    {
        未填写 = 1,
        已达成 = 2,
        未达成 = 3
    }

    public enum CycleTime
    {
        年度 = 1,
        季度 = 2,
    }

    public enum Stamps
    {
        受控 = 1,
        非受控 = 2,
        作废 = 3,
        现行有效 = 4
    }

    public enum LC_TimeType
    {
        入库作业 = 1,
        在库保管 = 2,
        出库分拣 = 3,
        领货出库 = 4
    }

    public enum LC_TimeStatus
    {
        开始 = 1,
        结束 = 2
    }

    public enum LC_ScanRecordType
    {
        入库扫码 = 1,
        出库扫码 = 2
    }

    public enum AchieveType
    {
        大于 = 1,
        大于等于 = 2,
        小于 = 3,
        小于等于 = 4
    }

    public static class GYCode
    {
        public static string MessageTitle = "MessageTitle";
        public static string MediaId = "MediaId";
        public static string DocMediaId = "DocMediaId";
    }
}
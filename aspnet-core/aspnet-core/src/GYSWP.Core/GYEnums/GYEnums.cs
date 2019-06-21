using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.GYEnums
{
    public enum ConfigType
    {
        钉钉配置 = 1,
        标准化工作平台 = 2
    }
    public enum ConfigModel
    {
        标准化工作平台 = 1,
        物流中心 =2,
        钉钉配置 =3
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
        标准制定 =4
    }

}



using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.DocAttachments;
using GYSWP.Dtos;
using GYSWP.LC_KyjFunctionRecords;


namespace GYSWP.LC_KyjFunctionRecords.DomainService
{
    public interface ILC_KyjFunctionRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_KyjFunctionRecord();

    }
}

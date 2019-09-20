
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYSWP.LC_TimeLogs;
using GYSWP.LC_TimeLogs.Dtos;
using GYSWP.LC_TimeLogs.DomainService;
using Abp.Auditing;
using GYSWP.Dtos;
using GYSWP.GYEnums;

namespace GYSWP.LC_TimeLogs
{
    /// <summary>
    /// LC_TimeLog应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_TimeLogAppService : GYSWPAppServiceBase, ILC_TimeLogAppService
    {
        private readonly IRepository<LC_TimeLog, Guid> _entityRepository;

        private readonly ILC_TimeLogManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_TimeLogAppService(
        IRepository<LC_TimeLog, Guid> entityRepository
        , ILC_TimeLogManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取LC_TimeLog的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_TimeLogListDto>> GetPaged(GetLC_TimeLogsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<LC_TimeLogListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<LC_TimeLogListDto>>();

            return new PagedResultDto<LC_TimeLogListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_TimeLogListDto信息
        /// </summary>

        public async Task<LC_TimeLogListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_TimeLogListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_TimeLog
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_TimeLogForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_TimeLogForEditOutput();
            LC_TimeLogEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_TimeLogEditDto>();

                //lC_TimeLogEditDto = ObjectMapper.Map<List<lC_TimeLogEditDto>>(entity);
            }
            else
            {
                editDto = new LC_TimeLogEditDto();
            }

            output.LC_TimeLog = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_TimeLog的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task<LC_TimeLogEditDto> CreateOrUpdate(CreateOrUpdateLC_TimeLogInput input)
        {

            if (input.LC_TimeLog.Id.HasValue)
            {
                return await Update(input.LC_TimeLog);
            }
            else
            {
                return await Create(input.LC_TimeLog);
            }
        }


        /// <summary>
        /// 新增LC_TimeLog
        /// </summary>

        protected virtual async Task<LC_TimeLogEditDto> Create(LC_TimeLogEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_TimeLog>(input);
            var entity = input.MapTo<LC_TimeLog>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_TimeLogEditDto>();
        }

        /// <summary>
        /// 编辑LC_TimeLog
        /// </summary>

        protected virtual async Task<LC_TimeLogEditDto> Update(LC_TimeLogEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            var item = await _entityRepository.UpdateAsync(entity);
            return item.MapTo<LC_TimeLogEditDto>();
        }



        /// <summary>
        /// 删除LC_TimeLog信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_TimeLog的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 记录开始入库信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateBeginInStorageAsync(CreateLC_TimeLogsInput input)
        {
            LC_TimeLog entity = new LC_TimeLog();
            entity.EmployeeId = input.EmployeeId;
            entity.EmployeeName = input.EmployeeName;
            entity.Type = GYEnums.LC_TimeType.入库作业;
            entity.Status = GYEnums.LC_TimeStatus.开始;
            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        }

        /// <summary>
        /// 扫码出库完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateScanOverAsync(CreateLC_TimeLogsInput input)
        {
            LC_TimeLog entity = new LC_TimeLog();
            entity.EmployeeId = input.EmployeeId;
            entity.Type = GYEnums.LC_TimeType.领货出库;
            entity.Status = GYEnums.LC_TimeStatus.结束;
            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        }

        /// <summary>
        /// 根据id修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> ModifyStatusById(Guid? id, LC_TimeStatus status)
        {
            if (id.HasValue)
            {
                LC_TimeLog lC_TimeLog = await _entityRepository.FirstOrDefaultAsync(id.Value);
                if (lC_TimeLog == null)
                {
                    return new APIResultDto() { Code = 1, Msg = "未找到当前项" };
                }
                else
                {
                    lC_TimeLog.Status = status;
                    lC_TimeLog.EndTime = DateTime.Now;
                    await _entityRepository.UpdateAsync(lC_TimeLog);
                    return new APIResultDto() { Code = 0, Msg = "修改状态成功" };
                }
            }
            else
            {
                return new APIResultDto() { Code = 0, Msg = "成功结束" };
            }
        }
    }
}
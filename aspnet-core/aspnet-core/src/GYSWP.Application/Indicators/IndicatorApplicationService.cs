
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


using GYSWP.Indicators;
using GYSWP.Indicators.Dtos;
using GYSWP.Indicators.DomainService;
using GYSWP.IndicatorsDetails;

namespace GYSWP.Indicators
{
    /// <summary>
    /// Indicator应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IndicatorAppService : GYSWPAppServiceBase, IIndicatorAppService
    {
        private readonly IRepository<Indicator, Guid> _entityRepository;
        private readonly IIndicatorManager _entityManager;
        private readonly IRepository<IndicatorsDetail, Guid> _indicatorsDetailRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorAppService(
        IRepository<Indicator, Guid> entityRepository
        , IIndicatorManager entityManager
        , IRepository<IndicatorsDetail, Guid> indicatorsDetailRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _indicatorsDetailRepository = indicatorsDetailRepository;
        }


        /// <summary>
        /// 获取Indicator的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<IndicatorListDto>> GetPaged(GetIndicatorsInput input)
        {
            var user = await GetCurrentUserAsync();
            //var detail =  _indicatorsDetailRepository.GetAll();
            var query = _entityRepository.GetAll().Where(v => v.CreatorEmpeeId == user.EmployeeId);
            //var query = from i in indicator
            //            join d in detail on i.Id equals d.IndicatorsId into g
            //            from table in g.DefaultIfEmpty()
            //            select new IndicatorShowDto() {
            //                Id = i.Id,
            //                CreationTime = i.CreationTime,
            //                CreatorEmpName = i.CreatorEmpName,
            //                Title = i.Title,
            //                Paraphrase = i.Paraphrase,
            //                MeasuringWay = i.MeasuringWay,
            //                ExpectedValue = i.ExpectedValue,
            //                CycleTimeName = i.CycleTime.ToString(),
            //                DeptIds = i.DeptId,
            //                StatusName = table.Status.ToString(),
            //                ActualValue = table.ActualValue,
            //                CreatorDeptName = i.CreatorDeptName,
            //                DeptNames = i.DeptName
            //            };

            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v=>v.CycleTime).ThenByDescending(v=>v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<IndicatorListDto>>();
            return new PagedResultDto<IndicatorListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取IndicatorListDto信息
        /// </summary>

        public async Task<IndicatorListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<IndicatorListDto>();
        }

        /// <summary>
        /// 获取编辑 Indicator
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetIndicatorForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetIndicatorForEditOutput();
            IndicatorEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<IndicatorEditDto>();

                //indicatorEditDto = ObjectMapper.Map<List<indicatorEditDto>>(entity);
            }
            else
            {
                editDto = new IndicatorEditDto();
            }

            output.Indicator = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Indicator的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateIndicatorInput input)
        {

            if (input.Indicator.Id.HasValue)
            {
                await Update(input.Indicator);
            }
            else
            {
                var result =  await Create(input.Indicator);
                await CurrentUnitOfWork.SaveChangesAsync();
                foreach (var item in input.DeptInfo)
                {
                    IndicatorsDetail detail = new IndicatorsDetail();
                    detail.IndicatorsId = result.Id.Value;
                    detail.DeptId = item.DeptId;
                    detail.DeptName = item.DeptName;
                    detail.Status = GYEnums.IndicatorStatus.未填写;
                    await _indicatorsDetailRepository.InsertAsync(detail);
                }
            }
        }


        /// <summary>
        /// 新增Indicator
        /// </summary>

        protected virtual async Task<IndicatorEditDto> Create(IndicatorEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Indicator>(input);
            var entity = input.MapTo<Indicator>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<IndicatorEditDto>();
        }

        /// <summary>
        /// 编辑Indicator
        /// </summary>

        protected virtual async Task Update(IndicatorEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Indicator信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Indicator的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }
    }
}
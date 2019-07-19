
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


using GYSWP.PositionInfos;
using GYSWP.PositionInfos.Dtos;
using GYSWP.PositionInfos.DomainService;
using GYSWP.MainPointsRecords;
using GYSWP.Dtos;

namespace GYSWP.PositionInfos
{
    /// <summary>
    /// MainPointsRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class MainPointsRecordAppService : GYSWPAppServiceBase, IMainPointsRecordAppService
    {
        private readonly IRepository<MainPointsRecord, Guid> _entityRepository;

        private readonly IMainPointsRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public MainPointsRecordAppService(
        IRepository<MainPointsRecord, Guid> entityRepository
        ,IMainPointsRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取MainPointsRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<MainPointsRecordListDto>> GetPaged(GetMainPointsRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<MainPointsRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<MainPointsRecordListDto>>();

			return new PagedResultDto<MainPointsRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取MainPointsRecordListDto信息
		/// </summary>
		 
		public async Task<MainPointsRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<MainPointsRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 MainPointsRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetMainPointsRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetMainPointsRecordForEditOutput();
MainPointsRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<MainPointsRecordEditDto>();

				//mainPointsRecordEditDto = ObjectMapper.Map<List<mainPointsRecordEditDto>>(entity);
			}
			else
			{
				editDto = new MainPointsRecordEditDto();
			}

			output.MainPointsRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改MainPointsRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateMainPointsRecordInput input)
		{

			if (input.MainPointsRecord.Id.HasValue)
			{
				await Update(input.MainPointsRecord);
			}
			else
			{
				await Create(input.MainPointsRecord);
			}
		}


		/// <summary>
		/// 新增MainPointsRecord
		/// </summary>
		
		protected virtual async Task<MainPointsRecordEditDto> Create(MainPointsRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <MainPointsRecord>(input);
            var entity=input.MapTo<MainPointsRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<MainPointsRecordEditDto>();
		}

		/// <summary>
		/// 编辑MainPointsRecord
		/// </summary>
		
		protected virtual async Task Update(MainPointsRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除MainPointsRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}

		/// <summary>
		/// 批量删除MainPointsRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}
        
        /// <summary>
        /// 新增工作职责信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         
        public async Task<APIResultDto> CreateMainPointRecordAsync(MainPointsRecordEditDto input)
        {
            var entity = input.MapTo<MainPointsRecord>();
            var id = await _entityRepository.InsertAndGetIdAsync(entity);
            return new APIResultDto
            {
                Code = 0,
                Data = id
            };
        }
    }
}

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


using GYSWP.InspectionRecords;
using GYSWP.InspectionRecords.Dtos;
using GYSWP.InspectionRecords.DomainService;
using Abp.Auditing;

namespace GYSWP.InspectionRecords
{
    /// <summary>
    /// InspectionRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class InspectionRecordAppService : GYSWPAppServiceBase, IInspectionRecordAppService
    {
        private readonly IRepository<InspectionRecord, long> _entityRepository;

        private readonly IInspectionRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public InspectionRecordAppService(
        IRepository<InspectionRecord, long> entityRepository
        ,IInspectionRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取InspectionRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<InspectionRecordListDto>> GetPagedAsync(GetInspectionRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<InspectionRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<InspectionRecordListDto>>();

			return new PagedResultDto<InspectionRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取InspectionRecordListDto信息
		/// </summary>
		 
		public async Task<InspectionRecordListDto> GetByIdAsync(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<InspectionRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 InspectionRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetInspectionRecordForEditOutput> GetForEditAsync(NullableIdDto<long> input)
		{
			var output = new GetInspectionRecordForEditOutput();
InspectionRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<InspectionRecordEditDto>();

				//inspectionRecordEditDto = ObjectMapper.Map<List<inspectionRecordEditDto>>(entity);
			}
			else
			{
				editDto = new InspectionRecordEditDto();
			}

			output.InspectionRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改InspectionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdateAsync(CreateOrUpdateInspectionRecordInput input)
		{

			if (input.InspectionRecord.Id.HasValue)
			{
				await Update(input.InspectionRecord);
			}
			else
			{
				await Create(input.InspectionRecord);
			}
		}


		/// <summary>
		/// 新增InspectionRecord
		/// </summary>
		
		protected virtual async Task<InspectionRecordEditDto> Create(InspectionRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <InspectionRecord>(input);
            var entity=input.MapTo<InspectionRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<InspectionRecordEditDto>();
		}

		/// <summary>
		/// 编辑InspectionRecord
		/// </summary>
		
		protected virtual async Task Update(InspectionRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除InspectionRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除InspectionRecord的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出InspectionRecord为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}

    }
}



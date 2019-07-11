
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


using GYSWP.LC_OutScanRecords;
using GYSWP.LC_OutScanRecords.Dtos;
using GYSWP.LC_OutScanRecords.DomainService;



namespace GYSWP.LC_OutScanRecords
{
    /// <summary>
    /// LC_OutScanRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_OutScanRecordAppService : GYSWPAppServiceBase, ILC_OutScanRecordAppService
    {
        private readonly IRepository<LC_OutScanRecord, Guid> _entityRepository;

        private readonly ILC_OutScanRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_OutScanRecordAppService(
        IRepository<LC_OutScanRecord, Guid> entityRepository
        ,ILC_OutScanRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_OutScanRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_OutScanRecordListDto>> GetPaged(GetLC_OutScanRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_OutScanRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_OutScanRecordListDto>>();

			return new PagedResultDto<LC_OutScanRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_OutScanRecordListDto信息
		/// </summary>
		 
		public async Task<LC_OutScanRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_OutScanRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_OutScanRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_OutScanRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_OutScanRecordForEditOutput();
LC_OutScanRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_OutScanRecordEditDto>();

				//lC_OutScanRecordEditDto = ObjectMapper.Map<List<lC_OutScanRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_OutScanRecordEditDto();
			}

			output.LC_OutScanRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_OutScanRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_OutScanRecordInput input)
		{

			if (input.LC_OutScanRecord.Id.HasValue)
			{
				await Update(input.LC_OutScanRecord);
			}
			else
			{
				await Create(input.LC_OutScanRecord);
			}
		}


		/// <summary>
		/// 新增LC_OutScanRecord
		/// </summary>
		
		protected virtual async Task<LC_OutScanRecordEditDto> Create(LC_OutScanRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_OutScanRecord>(input);
            var entity=input.MapTo<LC_OutScanRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_OutScanRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_OutScanRecord
		/// </summary>
		
		protected virtual async Task Update(LC_OutScanRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_OutScanRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_OutScanRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_OutScanRecord为excel表,等待开发。
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



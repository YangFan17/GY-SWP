
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


using GYSWP.LC_SsjMonthWhByRecords;
using GYSWP.LC_SsjMonthWhByRecords.Dtos;
using GYSWP.LC_SsjMonthWhByRecords.DomainService;



namespace GYSWP.LC_SsjMonthWhByRecords
{
    /// <summary>
    /// LC_SsjMonthWhByRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SsjMonthWhByRecordAppService : GYSWPAppServiceBase, ILC_SsjMonthWhByRecordAppService
    {
        private readonly IRepository<LC_SsjMonthWhByRecord, Guid> _entityRepository;

        private readonly ILC_SsjMonthWhByRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SsjMonthWhByRecordAppService(
        IRepository<LC_SsjMonthWhByRecord, Guid> entityRepository
        ,ILC_SsjMonthWhByRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_SsjMonthWhByRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_SsjMonthWhByRecordListDto>> GetPaged(GetLC_SsjMonthWhByRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_SsjMonthWhByRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_SsjMonthWhByRecordListDto>>();

			return new PagedResultDto<LC_SsjMonthWhByRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_SsjMonthWhByRecordListDto信息
		/// </summary>
		 
		public async Task<LC_SsjMonthWhByRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_SsjMonthWhByRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_SsjMonthWhByRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_SsjMonthWhByRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_SsjMonthWhByRecordForEditOutput();
LC_SsjMonthWhByRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_SsjMonthWhByRecordEditDto>();

				//lC_SsjMonthWhByRecordEditDto = ObjectMapper.Map<List<lC_SsjMonthWhByRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_SsjMonthWhByRecordEditDto();
			}

			output.LC_SsjMonthWhByRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_SsjMonthWhByRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_SsjMonthWhByRecordInput input)
		{

			if (input.LC_SsjMonthWhByRecord.Id.HasValue)
			{
				await Update(input.LC_SsjMonthWhByRecord);
			}
			else
			{
				await Create(input.LC_SsjMonthWhByRecord);
			}
		}


		/// <summary>
		/// 新增LC_SsjMonthWhByRecord
		/// </summary>
		
		protected virtual async Task<LC_SsjMonthWhByRecordEditDto> Create(LC_SsjMonthWhByRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SsjMonthWhByRecord>(input);
            var entity=input.MapTo<LC_SsjMonthWhByRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_SsjMonthWhByRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_SsjMonthWhByRecord
		/// </summary>
		
		protected virtual async Task Update(LC_SsjMonthWhByRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_SsjMonthWhByRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_SsjMonthWhByRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_SsjMonthWhByRecord为excel表,等待开发。
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



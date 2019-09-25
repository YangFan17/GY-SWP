
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


using GYSWP.LC_KyjWeekMaintainRecords;
using GYSWP.LC_KyjWeekMaintainRecords.Dtos;
using GYSWP.LC_KyjWeekMaintainRecords.DomainService;
using GYSWP.Dtos;

namespace GYSWP.LC_KyjWeekMaintainRecords
{
    /// <summary>
    /// LC_KyjWeekMaintainRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_KyjWeekMaintainRecordAppService : GYSWPAppServiceBase, ILC_KyjWeekMaintainRecordAppService
    {
        private readonly IRepository<LC_KyjWeekMaintainRecord, Guid> _entityRepository;

        private readonly ILC_KyjWeekMaintainRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_KyjWeekMaintainRecordAppService(
        IRepository<LC_KyjWeekMaintainRecord, Guid> entityRepository
        ,ILC_KyjWeekMaintainRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_KyjWeekMaintainRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_KyjWeekMaintainRecordListDto>> GetPaged(GetLC_KyjWeekMaintainRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_KyjWeekMaintainRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_KyjWeekMaintainRecordListDto>>();

			return new PagedResultDto<LC_KyjWeekMaintainRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_KyjWeekMaintainRecordListDto信息
		/// </summary>
		 
		public async Task<LC_KyjWeekMaintainRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_KyjWeekMaintainRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_KyjWeekMaintainRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_KyjWeekMaintainRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_KyjWeekMaintainRecordForEditOutput();
LC_KyjWeekMaintainRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_KyjWeekMaintainRecordEditDto>();

				//lC_KyjWeekMaintainRecordEditDto = ObjectMapper.Map<List<lC_KyjWeekMaintainRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_KyjWeekMaintainRecordEditDto();
			}

			output.LC_KyjWeekMaintainRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_KyjWeekMaintainRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_KyjWeekMaintainRecordInput input)
		{

			if (input.LC_KyjWeekMaintainRecord.Id.HasValue)
			{
				await Update(input.LC_KyjWeekMaintainRecord);
			}
			else
			{
				await Create(input.LC_KyjWeekMaintainRecord);
			}
		}


		/// <summary>
		/// 新增LC_KyjWeekMaintainRecord
		/// </summary>
		
		protected virtual async Task<LC_KyjWeekMaintainRecordEditDto> Create(LC_KyjWeekMaintainRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_KyjWeekMaintainRecord>(input);
            var entity=input.MapTo<LC_KyjWeekMaintainRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_KyjWeekMaintainRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_KyjWeekMaintainRecord
		/// </summary>
		
		protected virtual async Task Update(LC_KyjWeekMaintainRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_KyjWeekMaintainRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_KyjWeekMaintainRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 钉钉创建LC_KyjWeekMaintainRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateKyjWeekMaintainRecordAsync(LC_KyjWeekMaintainRecordEditDto input)
        {
            var entity = input.MapTo<LC_KyjWeekMaintainRecord>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }


        /// <summary>
        /// 导出LC_KyjWeekMaintainRecord为excel表,等待开发。
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



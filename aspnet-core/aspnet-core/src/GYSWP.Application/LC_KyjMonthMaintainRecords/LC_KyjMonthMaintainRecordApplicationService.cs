
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


using GYSWP.LC_KyjMonthMaintainRecords;
using GYSWP.LC_KyjMonthMaintainRecords.Dtos;
using GYSWP.LC_KyjMonthMaintainRecords.DomainService;
using GYSWP.Dtos;

namespace GYSWP.LC_KyjMonthMaintainRecords
{
    /// <summary>
    /// LC_KyjMonthMaintainRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_KyjMonthMaintainRecordAppService : GYSWPAppServiceBase, ILC_KyjMonthMaintainRecordAppService
    {
        private readonly IRepository<LC_KyjMonthMaintainRecord, Guid> _entityRepository;

        private readonly ILC_KyjMonthMaintainRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_KyjMonthMaintainRecordAppService(
        IRepository<LC_KyjMonthMaintainRecord, Guid> entityRepository
        ,ILC_KyjMonthMaintainRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_KyjMonthMaintainRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_KyjMonthMaintainRecordListDto>> GetPaged(GetLC_KyjMonthMaintainRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_KyjMonthMaintainRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_KyjMonthMaintainRecordListDto>>();

			return new PagedResultDto<LC_KyjMonthMaintainRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_KyjMonthMaintainRecordListDto信息
		/// </summary>
		 
		public async Task<LC_KyjMonthMaintainRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_KyjMonthMaintainRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_KyjMonthMaintainRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_KyjMonthMaintainRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_KyjMonthMaintainRecordForEditOutput();
LC_KyjMonthMaintainRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_KyjMonthMaintainRecordEditDto>();

				//lC_KyjMonthMaintainRecordEditDto = ObjectMapper.Map<List<lC_KyjMonthMaintainRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_KyjMonthMaintainRecordEditDto();
			}

			output.LC_KyjMonthMaintainRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_KyjMonthMaintainRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_KyjMonthMaintainRecordInput input)
		{

			if (input.LC_KyjMonthMaintainRecord.Id.HasValue)
			{
				await Update(input.LC_KyjMonthMaintainRecord);
			}
			else
			{
				await Create(input.LC_KyjMonthMaintainRecord);
			}
		}


		/// <summary>
		/// 新增LC_KyjMonthMaintainRecord
		/// </summary>
		
		protected virtual async Task<LC_KyjMonthMaintainRecordEditDto> Create(LC_KyjMonthMaintainRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_KyjMonthMaintainRecord>(input);
            var entity=input.MapTo<LC_KyjMonthMaintainRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_KyjMonthMaintainRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_KyjMonthMaintainRecord
		/// </summary>
		
		protected virtual async Task Update(LC_KyjMonthMaintainRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_KyjMonthMaintainRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_KyjMonthMaintainRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 钉钉创建LC_KyjMonthMaintainRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateKyjMonthMaintainRecordAsync(LC_KyjMonthMaintainRecordEditDto input)
        {
            var entity = input.MapTo<LC_KyjMonthMaintainRecord>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }


        /// <summary>
        /// 导出LC_KyjMonthMaintainRecord为excel表,等待开发。
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




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


using GYSWP.LC_LPMaintainRecords;
using GYSWP.LC_LPMaintainRecords.Dtos;
using GYSWP.LC_LPMaintainRecords.DomainService;
using GYSWP.Dtos;
using GYSWP.DocAttachments;

namespace GYSWP.LC_LPMaintainRecords
{
    /// <summary>
    /// LC_LPMaintainRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_LPMaintainRecordAppService : GYSWPAppServiceBase, ILC_LPMaintainRecordAppService
    {
        private readonly IRepository<LC_LPMaintainRecord, Guid> _entityRepository;
        private readonly IRepository<LC_Attachment, Guid> _attachmentRepository;

        private readonly ILC_LPMaintainRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_LPMaintainRecordAppService(
        IRepository<LC_LPMaintainRecord, Guid> entityRepository
        ,ILC_LPMaintainRecordManager entityManager
        , IRepository<LC_Attachment, Guid> attachmentRepository
        )
        {
            _attachmentRepository = attachmentRepository;
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_LPMaintainRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_LPMaintainRecordListDto>> GetPaged(GetLC_LPMaintainRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_LPMaintainRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_LPMaintainRecordListDto>>();

			return new PagedResultDto<LC_LPMaintainRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_LPMaintainRecordListDto信息
		/// </summary>
		 
		public async Task<LC_LPMaintainRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_LPMaintainRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_LPMaintainRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_LPMaintainRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_LPMaintainRecordForEditOutput();
LC_LPMaintainRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_LPMaintainRecordEditDto>();

				//lC_LPMaintainRecordEditDto = ObjectMapper.Map<List<lC_LPMaintainRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_LPMaintainRecordEditDto();
			}

			output.LC_LPMaintainRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_LPMaintainRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_LPMaintainRecordInput input)
		{

			if (input.LC_LPMaintainRecord.Id.HasValue)
			{
				await Update(input.LC_LPMaintainRecord);
			}
			else
			{
				await Create(input.LC_LPMaintainRecord);
			}
		}


		/// <summary>
		/// 新增LC_LPMaintainRecord
		/// </summary>
		
		protected virtual async Task<LC_LPMaintainRecordEditDto> Create(LC_LPMaintainRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_LPMaintainRecord>(input);
            var entity=input.MapTo<LC_LPMaintainRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_LPMaintainRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_LPMaintainRecord
		/// </summary>
		
		protected virtual async Task Update(LC_LPMaintainRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_LPMaintainRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_LPMaintainRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 钉钉创建LC_LPMaintainRecord,并保存图片至LC_Attachment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateLPMaintainRecordAsync(DDCreateOrUpdateLC_LPMaintainRecordInput input)
        {
            var entity = input.LC_LPMaintainRecord.MapTo<LC_LPMaintainRecord>();

            entity = await _entityRepository.InsertAsync(entity);
            foreach (var path in input.DDAttachmentEditDto.Path)
            {
                var item = new LC_Attachment();
                item.Path = path;
                item.EmployeeId = input.DDAttachmentEditDto.EmployeeId;
                item.Type = input.DDAttachmentEditDto.Type;
                item.BLL = entity.Id;
                item.Remark = input.DDAttachmentEditDto.Remark;
                await _attachmentRepository.InsertAsync(item);
            }
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }


        /// <summary>
        /// 导出LC_LPMaintainRecord为excel表,等待开发。
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



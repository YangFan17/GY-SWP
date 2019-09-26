
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


using GYSWP.LC_ForkliftWeekWhRecords;
using GYSWP.LC_ForkliftWeekWhRecords.Dtos;
using GYSWP.LC_ForkliftWeekWhRecords.DomainService;
using GYSWP.DocAttachments;

namespace GYSWP.LC_ForkliftWeekWhRecords
{
    /// <summary>
    /// LC_ForkliftWeekWhRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ForkliftWeekWhRecordAppService : GYSWPAppServiceBase, ILC_ForkliftWeekWhRecordAppService
    {
        private readonly IRepository<LC_ForkliftWeekWhRecord, Guid> _entityRepository;

        private readonly ILC_ForkliftWeekWhRecordManager _entityManager;

        private readonly IRepository<LC_Attachment, Guid> _AttachmententityRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ForkliftWeekWhRecordAppService(
        IRepository<LC_ForkliftWeekWhRecord, Guid> entityRepository,
             IRepository<LC_Attachment, Guid> AttachRepository
        , ILC_ForkliftWeekWhRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _AttachmententityRepository = AttachRepository;
        }


        /// <summary>
        /// 获取LC_ForkliftWeekWhRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_ForkliftWeekWhRecordListDto>> GetPaged(GetLC_ForkliftWeekWhRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_ForkliftWeekWhRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_ForkliftWeekWhRecordListDto>>();

			return new PagedResultDto<LC_ForkliftWeekWhRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ForkliftWeekWhRecordListDto信息
		/// </summary>
		 
		public async Task<LC_ForkliftWeekWhRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ForkliftWeekWhRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ForkliftWeekWhRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ForkliftWeekWhRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ForkliftWeekWhRecordForEditOutput();
LC_ForkliftWeekWhRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ForkliftWeekWhRecordEditDto>();

				//lC_ForkliftWeekWhRecordEditDto = ObjectMapper.Map<List<lC_ForkliftWeekWhRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ForkliftWeekWhRecordEditDto();
			}

			output.LC_ForkliftWeekWhRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_ForkliftWeekWhRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateOrUpdate(CreateOrUpdateLC_ForkliftWeekWhRecordInput input)
		{

			if (input.LC_ForkliftWeekWhRecord.Id.HasValue)
			{
				await Update(input.LC_ForkliftWeekWhRecord);
			}
			else
			{
				await Create(input.LC_ForkliftWeekWhRecord);
			}
		}


		/// <summary>
		/// 新增LC_ForkliftWeekWhRecord
		/// </summary>
		
		protected virtual async Task<LC_ForkliftWeekWhRecordEditDto> Create(LC_ForkliftWeekWhRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ForkliftWeekWhRecord>(input);
            var entity=input.MapTo<LC_ForkliftWeekWhRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ForkliftWeekWhRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_ForkliftWeekWhRecord
		/// </summary>
		
		protected virtual async Task Update(LC_ForkliftWeekWhRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ForkliftWeekWhRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ForkliftWeekWhRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_ForkliftWeekWhRecord为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        /// <summary>
        /// 保养记录和照片拍照记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public virtual async Task RecordInsert(InsertLC_ForkliftWeekWhRecordInput input)
        {
            var entity = input.LC_ForkliftWeekWhRecord.MapTo<LC_ForkliftWeekWhRecord>();
            var returnId = await _entityRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            input.LC_ForkliftWeekWhRecord.BLL = returnId;
            if (input.LC_ForkliftWeekWhRecord.Path != null)
            {
                foreach (var item in input.LC_ForkliftWeekWhRecord.Path)
                {
                    var AttachEntity = new LC_Attachment();
                    AttachEntity.Path = item;
                    AttachEntity.EmployeeId = input.LC_ForkliftWeekWhRecord.EmployeeId;
                    AttachEntity.Type = input.LC_ForkliftWeekWhRecord.Type;
                    AttachEntity.Remark = input.LC_ForkliftWeekWhRecord.Remark;
                    AttachEntity.BLL = returnId;
                    await _AttachmententityRepository.InsertAsync(AttachEntity);
                }
            }
        }

    }
}



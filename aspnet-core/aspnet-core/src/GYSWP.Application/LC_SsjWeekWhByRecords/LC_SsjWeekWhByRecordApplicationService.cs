
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


using GYSWP.LC_SsjWeekWhByRecords;
using GYSWP.LC_SsjWeekWhByRecords.Dtos;
using GYSWP.LC_SsjWeekWhByRecords.DomainService;
using GYSWP.DocAttachments;

namespace GYSWP.LC_SsjWeekWhByRecords
{
    /// <summary>
    /// LC_SsjWeekWhByRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SsjWeekWhByRecordAppService : GYSWPAppServiceBase, ILC_SsjWeekWhByRecordAppService
    {
        private readonly IRepository<LC_SsjWeekWhByRecord, Guid> _entityRepository;

        private readonly ILC_SsjWeekWhByRecordManager _entityManager;

        private readonly IRepository<LC_Attachment, Guid> _AttachmententityRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SsjWeekWhByRecordAppService(
        IRepository<LC_SsjWeekWhByRecord, Guid> entityRepository
            , IRepository<LC_Attachment, Guid> AttachRepository
        , ILC_SsjWeekWhByRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _AttachmententityRepository = AttachRepository;
        }


        /// <summary>
        /// 获取LC_SsjWeekWhByRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_SsjWeekWhByRecordListDto>> GetPaged(GetLC_SsjWeekWhByRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_SsjWeekWhByRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_SsjWeekWhByRecordListDto>>();

			return new PagedResultDto<LC_SsjWeekWhByRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_SsjWeekWhByRecordListDto信息
		/// </summary>
		 
		public async Task<LC_SsjWeekWhByRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_SsjWeekWhByRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_SsjWeekWhByRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_SsjWeekWhByRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_SsjWeekWhByRecordForEditOutput();
LC_SsjWeekWhByRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_SsjWeekWhByRecordEditDto>();

				//lC_SsjWeekWhByRecordEditDto = ObjectMapper.Map<List<lC_SsjWeekWhByRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_SsjWeekWhByRecordEditDto();
			}

			output.LC_SsjWeekWhByRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_SsjWeekWhByRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAllowAnonymous]
		public async Task CreateOrUpdate(CreateOrUpdateLC_SsjWeekWhByRecordInput input)
		{

			if (input.LC_SsjWeekWhByRecord.Id.HasValue)
			{
				await Update(input.LC_SsjWeekWhByRecord);
			}
			else
			{
				await Create(input.LC_SsjWeekWhByRecord);
			}
		}


		/// <summary>
		/// 新增LC_SsjWeekWhByRecord
		/// </summary>
		
		protected virtual async Task<LC_SsjWeekWhByRecordEditDto> Create(LC_SsjWeekWhByRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SsjWeekWhByRecord>(input);
            var entity=input.MapTo<LC_SsjWeekWhByRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_SsjWeekWhByRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_SsjWeekWhByRecord
		/// </summary>
		
		protected virtual async Task Update(LC_SsjWeekWhByRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_SsjWeekWhByRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_SsjWeekWhByRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_SsjWeekWhByRecord为excel表,等待开发。
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
        public virtual async Task RecordInsert(InsertLC_SsjWeekWhByRecordInput input)
        {

            var entity = input.LC_SsjWeekWhByRecord.MapTo<LC_SsjWeekWhByRecord>();
            var returnId = await _entityRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            input.LC_SsjWeekWhByRecord.BLL = returnId;
            if (input.LC_SsjWeekWhByRecord.Path != null)
            {
                foreach (var item in input.LC_SsjWeekWhByRecord.Path)
                {
                    var AttachEntity = new LC_Attachment();
                    AttachEntity.Path = item;
                    AttachEntity.EmployeeId = input.LC_SsjWeekWhByRecord.EmployeeId;
                    AttachEntity.Type = input.LC_SsjWeekWhByRecord.Type;
                    AttachEntity.Remark = input.LC_SsjWeekWhByRecord.Remark;
                    AttachEntity.BLL = returnId;
                    await _AttachmententityRepository.InsertAsync(AttachEntity);
                }
            }
        }


    }
}




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


using GYSWP.LC_LPFunctionRecords;
using GYSWP.LC_LPFunctionRecords.Dtos;
using GYSWP.LC_LPFunctionRecords.DomainService;
using GYSWP.Dtos;
using GYSWP.DocAttachments;

namespace GYSWP.LC_LPFunctionRecords
{
    /// <summary>
    /// LC_LPFunctionRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_LPFunctionRecordAppService : GYSWPAppServiceBase, ILC_LPFunctionRecordAppService
    {
        private readonly IRepository<LC_LPFunctionRecord, Guid> _entityRepository;
        private readonly IRepository<LC_Attachment, Guid> _attachmentRepository;

        private readonly ILC_LPFunctionRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_LPFunctionRecordAppService(
        IRepository<LC_LPFunctionRecord, Guid> entityRepository
        ,ILC_LPFunctionRecordManager entityManager
            , IRepository<LC_Attachment, Guid> attachmentRepository
        )
        {
            _attachmentRepository = attachmentRepository;
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_LPFunctionRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_LPFunctionRecordListDto>> GetPaged(GetLC_LPFunctionRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_LPFunctionRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_LPFunctionRecordListDto>>();

			return new PagedResultDto<LC_LPFunctionRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_LPFunctionRecordListDto信息
		/// </summary>
		 
		public async Task<LC_LPFunctionRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_LPFunctionRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_LPFunctionRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_LPFunctionRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_LPFunctionRecordForEditOutput();
LC_LPFunctionRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_LPFunctionRecordEditDto>();

				//lC_LPFunctionRecordEditDto = ObjectMapper.Map<List<lC_LPFunctionRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_LPFunctionRecordEditDto();
			}

			output.LC_LPFunctionRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_LPFunctionRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_LPFunctionRecordInput input)
		{

			if (input.LC_LPFunctionRecord.Id.HasValue)
			{
				await Update(input.LC_LPFunctionRecord);
			}
			else
			{
				await Create(input.LC_LPFunctionRecord);
			}
		}


		/// <summary>
		/// 新增LC_LPFunctionRecord
		/// </summary>
		
		protected virtual async Task<LC_LPFunctionRecordEditDto> Create(LC_LPFunctionRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_LPFunctionRecord>(input);
            var entity=input.MapTo<LC_LPFunctionRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_LPFunctionRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_LPFunctionRecord
		/// </summary>
		
		protected virtual async Task Update(LC_LPFunctionRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_LPFunctionRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_LPFunctionRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 钉钉创建LC_LPFunctionRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateLPFunctionRecordAsync(LC_LPFunctionRecordEditDto input)
        {
            var entity = input.MapTo<LC_LPFunctionRecord>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }

        /// <summary>
        /// 钉钉通过指定条件获取LC_LPFunctionRecordListDto信息
        /// </summary>
        [AbpAllowAnonymous]
        public async Task<LC_LPFunctionRecordListDto> GetByWhere(string employeeId, DateTime useTime)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(aa => aa.EmployeeId == employeeId && aa.UseTime == useTime);

            return entity.MapTo<LC_LPFunctionRecordListDto>();
        }

        /// <summary>
        /// 钉钉编辑LC_LPFunctionRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> ModifyLPFunctionRecordAsync(LC_LPFunctionRecordEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }

        /// <summary>
        /// 钉钉添加或者修改LC_LPFunctionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateOrUpdateByDDAsync(DDCreateOrUpdateLC_LPFunctionRecordInput input)
        {

            if (input.LC_LPFunctionRecord.Id.HasValue)
            {
                return await DDUpdate(input);
            }
            else
            {
                return await DDCreate(input);
            }
        }

        /// <summary>
        /// 钉钉创建LC_LPFunctionRecord,并保存图片至LC_Attachment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task<APIResultDto> DDCreate(DDCreateOrUpdateLC_LPFunctionRecordInput input)
        {
            var entity = input.LC_LPFunctionRecord.MapTo<LC_LPFunctionRecord>();
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
        /// 钉钉编辑LC_LPFunctionRecord,并保存图片至LC_Attachment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task<APIResultDto> DDUpdate(DDCreateOrUpdateLC_LPFunctionRecordInput input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.LC_LPFunctionRecord.Id.Value);
            input.LC_LPFunctionRecord.MapTo(entity);
            foreach (var path in input.DDAttachmentEditDto.Path)
            {
                var attachment = await _attachmentRepository.CountAsync(aa => aa.Path == path);
                if (attachment <= 0)
                {
                    var item = new LC_Attachment();
                    item.Path = path;
                    item.EmployeeId = input.DDAttachmentEditDto.EmployeeId;
                    item.BLL = entity.Id;
                    item.Type = input.DDAttachmentEditDto.Type;
                    item.Remark = input.DDAttachmentEditDto.Remark;
                    await _attachmentRepository.InsertAsync(item);
                }
            }
            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }

        /// <summary>
        /// 钉钉通过指定条件获取LC_LPFunctionRecordListDto信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LC_LPFunctionRecordListDto> GetByDDWhereAsync(string employeeId, string remark)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(aa => aa.EmployeeId == employeeId && aa.CreationTime.ToString().Contains(DateTime.Now.ToShortDateString()));

            var item = entity.MapTo<LC_LPFunctionRecordListDto>();
            item.Path = await _attachmentRepository.GetAll().Where(aa => aa.BLL == entity.Id && aa.Remark == remark).Select(aa => aa.Path).AsNoTracking().ToArrayAsync();
            return item;
        }

        /// <summary>
        /// 导出LC_LPFunctionRecord为excel表,等待开发。
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



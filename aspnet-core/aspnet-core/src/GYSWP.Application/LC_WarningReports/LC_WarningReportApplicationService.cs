
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


using GYSWP.LC_WarningReports;
using GYSWP.LC_WarningReports.Dtos;

using GYSWP.LC_WarningReports.Authorization;


namespace GYSWP.LC_WarningReports
{
    /// <summary>
    /// LC_WarningReport应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_WarningReportAppService : GYSWPAppServiceBase, ILC_WarningReportAppService
    {
        private readonly IRepository<LC_WarningReport, Guid> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_WarningReportAppService(
        IRepository<LC_WarningReport, Guid> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取LC_WarningReport的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAuthorize(LC_WarningReportPermissions.Query)] 
        public async Task<PagedResultDto<LC_WarningReportListDto>> GetPaged(GetLC_WarningReportsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_WarningReportListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_WarningReportListDto>>();

			return new PagedResultDto<LC_WarningReportListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_WarningReportListDto信息
		/// </summary>
		[AbpAuthorize(LC_WarningReportPermissions.Query)] 
		public async Task<LC_WarningReportListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_WarningReportListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_WarningReport
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(LC_WarningReportPermissions.Create,LC_WarningReportPermissions.Edit)]
		public async Task<GetLC_WarningReportForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_WarningReportForEditOutput();
LC_WarningReportEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_WarningReportEditDto>();

				//lC_WarningReportEditDto = ObjectMapper.Map<List<lC_WarningReportEditDto>>(entity);
			}
			else
			{
				editDto = new LC_WarningReportEditDto();
			}

			output.LC_WarningReport = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_WarningReport的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateOrUpdate(CreateOrUpdateLC_WarningReportInput input)
		{

			if (input.LC_WarningReport.Id.HasValue)
			{
				await Update(input.LC_WarningReport);
			}
			else
			{
				await Create(input.LC_WarningReport);
			}
		}


		/// <summary>
		/// 新增LC_WarningReport
		/// </summary>
		protected virtual async Task<LC_WarningReportEditDto> Create(LC_WarningReportEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_WarningReport>(input);
            var entity=input.MapTo<LC_WarningReport>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_WarningReportEditDto>();
		}

		/// <summary>
		/// 编辑LC_WarningReport
		/// </summary>
		protected virtual async Task Update(LC_WarningReportEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_WarningReport信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(LC_WarningReportPermissions.Delete)]
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_WarningReport的方法
		/// </summary>
		[AbpAuthorize(LC_WarningReportPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_WarningReport为excel表,等待开发。
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



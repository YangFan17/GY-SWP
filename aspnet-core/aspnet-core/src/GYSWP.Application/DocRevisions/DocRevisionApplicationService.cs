
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


using GYSWP.DocRevisions;
using GYSWP.DocRevisions.Dtos;
using GYSWP.DocRevisions.DomainService;
using GYSWP.Dtos;
using GYSWP.Employees;

namespace GYSWP.DocRevisions
{
    /// <summary>
    /// DocRevision应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DocRevisionAppService : GYSWPAppServiceBase, IDocRevisionAppService
    {
        private readonly IRepository<DocRevision, Guid> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IDocRevisionManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DocRevisionAppService(
        IRepository<DocRevision, Guid> entityRepository
        ,IDocRevisionManager entityManager
        , IRepository<Employee, string> employeeRepository
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 获取DocRevision的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DocRevisionListDto>> GetPaged(GetDocRevisionsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<DocRevisionListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<DocRevisionListDto>>();

			return new PagedResultDto<DocRevisionListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取DocRevisionListDto信息
		/// </summary>
		 
		public async Task<DocRevisionListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAll().Where(v=>v.ApplyInfoId == input.Id).AsNoTracking().FirstOrDefaultAsync();
		    return entity.MapTo<DocRevisionListDto>();
		}

		/// <summary>
		/// 获取编辑 DocRevision
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetDocRevisionForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetDocRevisionForEditOutput();
DocRevisionEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<DocRevisionEditDto>();

				//docRevisionEditDto = ObjectMapper.Map<List<docRevisionEditDto>>(entity);
			}
			else
			{
				editDto = new DocRevisionEditDto();
			}

			output.DocRevision = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改DocRevision的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocRevisionInput input)
		{
            //if (input.DocRevision.Id.HasValue)
            //{
            //	await Update(input.DocRevision);
            //}
            //else
            //{
            //	await Create(input.DocRevision);
            //}
            if (input.DocRevision.Id.HasValue)
            {
                var entity = await Update(input.DocRevision);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity };
            }
            else
            {
                var user = await GetCurrentUserAsync();
                string dept = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
                string deptId = dept.Replace('[', ' ').Replace(']', ' ').Trim();
                input.DocRevision.EmployeeId = user.EmployeeId;
                input.DocRevision.EmployeeName = user.EmployeeName;
                input.DocRevision.Status = GYEnums.RevisionStatus.等待提交;
                input.DocRevision.RevisionType = GYEnums.RevisionType.标准制定;
                input.DocRevision.DeptId = deptId;
                var entity = await Create(input.DocRevision);
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity };
            }
        }


		/// <summary>
		/// 新增DocRevision
		/// </summary>
		
		protected virtual async Task<DocRevisionEditDto> Create(DocRevisionEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <DocRevision>(input);
            var entity=input.MapTo<DocRevision>();
			

			entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
			return entity.MapTo<DocRevisionEditDto>();
		}

		/// <summary>
		/// 编辑DocRevision
		/// </summary>
		
		protected virtual async Task<DocRevisionEditDto> Update(DocRevisionEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<DocRevisionEditDto>();
        }



        /// <summary>
        /// 删除DocRevision信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除DocRevision的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出DocRevision为excel表,等待开发。
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



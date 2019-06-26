
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


using GYSWP.CriterionExamines;
using GYSWP.CriterionExamines.Dtos;
using GYSWP.CriterionExamines.DomainService;



namespace GYSWP.CriterionExamines
{
    /// <summary>
    /// CriterionExamine应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class CriterionExamineAppService : GYSWPAppServiceBase, ICriterionExamineAppService
    {
        private readonly IRepository<CriterionExamine, Guid> _entityRepository;

        private readonly ICriterionExamineManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CriterionExamineAppService(
        IRepository<CriterionExamine, Guid> entityRepository
        ,ICriterionExamineManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取CriterionExamine的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<CriterionExamineListDto>> GetPaged(GetCriterionExaminesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<CriterionExamineListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<CriterionExamineListDto>>();

			return new PagedResultDto<CriterionExamineListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取CriterionExamineListDto信息
		/// </summary>
		 
		public async Task<CriterionExamineListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<CriterionExamineListDto>();
		}

		/// <summary>
		/// 获取编辑 CriterionExamine
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetCriterionExamineForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetCriterionExamineForEditOutput();
CriterionExamineEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<CriterionExamineEditDto>();

				//criterionExamineEditDto = ObjectMapper.Map<List<criterionExamineEditDto>>(entity);
			}
			else
			{
				editDto = new CriterionExamineEditDto();
			}

			output.CriterionExamine = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CriterionExamine的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateCriterionExamineInput input)
		{

			if (input.CriterionExamine.Id.HasValue)
			{
				await Update(input.CriterionExamine);
			}
			else
			{
				await Create(input.CriterionExamine);
			}
		}


		/// <summary>
		/// 新增CriterionExamine
		/// </summary>
		
		protected virtual async Task<CriterionExamineEditDto> Create(CriterionExamineEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <CriterionExamine>(input);
            var entity=input.MapTo<CriterionExamine>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<CriterionExamineEditDto>();
		}

		/// <summary>
		/// 编辑CriterionExamine
		/// </summary>
		
		protected virtual async Task Update(CriterionExamineEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CriterionExamine信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CriterionExamine的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出CriterionExamine为excel表,等待开发。
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



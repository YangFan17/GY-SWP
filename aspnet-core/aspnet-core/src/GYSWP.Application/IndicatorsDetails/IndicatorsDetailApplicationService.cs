
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


using GYSWP.IndicatorsDetails;
using GYSWP.IndicatorsDetails.Dtos;
using GYSWP.IndicatorsDetails.DomainService;



namespace GYSWP.IndicatorsDetails
{
    /// <summary>
    /// IndicatorsDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IndicatorsDetailAppService : GYSWPAppServiceBase, IIndicatorsDetailAppService
    {
        private readonly IRepository<IndicatorsDetail, Guid> _entityRepository;

        private readonly IIndicatorsDetailManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorsDetailAppService(
        IRepository<IndicatorsDetail, Guid> entityRepository
        ,IIndicatorsDetailManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取IndicatorsDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<IndicatorsDetailListDto>> GetPaged(GetIndicatorsDetailsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<IndicatorsDetailListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<IndicatorsDetailListDto>>();

			return new PagedResultDto<IndicatorsDetailListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取IndicatorsDetailListDto信息
		/// </summary>
		 
		public async Task<IndicatorsDetailListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<IndicatorsDetailListDto>();
		}

		/// <summary>
		/// 获取编辑 IndicatorsDetail
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetIndicatorsDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetIndicatorsDetailForEditOutput();
IndicatorsDetailEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<IndicatorsDetailEditDto>();

				//indicatorsDetailEditDto = ObjectMapper.Map<List<indicatorsDetailEditDto>>(entity);
			}
			else
			{
				editDto = new IndicatorsDetailEditDto();
			}

			output.IndicatorsDetail = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改IndicatorsDetail的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateIndicatorsDetailInput input)
		{

			if (input.IndicatorsDetail.Id.HasValue)
			{
				await Update(input.IndicatorsDetail);
			}
			else
			{
				await Create(input.IndicatorsDetail);
			}
		}


		/// <summary>
		/// 新增IndicatorsDetail
		/// </summary>
		
		protected virtual async Task<IndicatorsDetailEditDto> Create(IndicatorsDetailEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <IndicatorsDetail>(input);
            var entity=input.MapTo<IndicatorsDetail>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<IndicatorsDetailEditDto>();
		}

		/// <summary>
		/// 编辑IndicatorsDetail
		/// </summary>
		
		protected virtual async Task Update(IndicatorsDetailEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除IndicatorsDetail信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除IndicatorsDetail的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出IndicatorsDetail为excel表,等待开发。
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



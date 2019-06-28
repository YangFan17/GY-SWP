
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


using GYSWP.ExamineResults;
using GYSWP.ExamineResults.Dtos;
using GYSWP.ExamineResults.DomainService;
using GYSWP.Dtos;
using GYSWP.ExamineDetails;

namespace GYSWP.ExamineResults
{
    /// <summary>
    /// ExamineResult应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ExamineResultAppService : GYSWPAppServiceBase, IExamineResultAppService
    {
        private readonly IRepository<ExamineResult, Guid> _entityRepository;
        private readonly IRepository<ExamineDetail, Guid> _examineDetailRepository;
        private readonly IExamineResultManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExamineResultAppService(
        IRepository<ExamineResult, Guid> entityRepository
        ,IExamineResultManager entityManager
        , IRepository<ExamineDetail, Guid> examineDetailRepository
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _examineDetailRepository = examineDetailRepository;
        }


        /// <summary>
        /// 获取ExamineResult的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ExamineResultListDto>> GetPaged(GetExamineResultsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ExamineResultListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ExamineResultListDto>>();

			return new PagedResultDto<ExamineResultListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ExamineResultListDto信息
		/// </summary>
		 
		public async Task<ExamineResultListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ExamineResultListDto>();
		}

		/// <summary>
		/// 获取编辑 ExamineResult
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetExamineResultForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetExamineResultForEditOutput();
ExamineResultEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ExamineResultEditDto>();

				//examineResultEditDto = ObjectMapper.Map<List<examineResultEditDto>>(entity);
			}
			else
			{
				editDto = new ExamineResultEditDto();
			}

			output.ExamineResult = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ExamineResult的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateExamineResultInput input)
		{
			if (input.ExamineResult.Id.HasValue)
			{
				await Update(input.ExamineResult);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
			{
                var user = await GetCurrentUserAsync();
                input.ExamineResult.EmployeeId = user.EmployeeId;
                input.ExamineResult.EmployeeName = user.EmployeeName;
                var entity = await Create(input.ExamineResult);
                var examineDetail = await _examineDetailRepository.FirstOrDefaultAsync(v => v.Id == input.ExamineResult.ExamineDetailId);
                examineDetail.Status = GYEnums.ResultStatus.已完成;
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }


		/// <summary>
		/// 新增ExamineResult
		/// </summary>
		
		protected virtual async Task<ExamineResultEditDto> Create(ExamineResultEditDto input)
		{
            var entity=input.MapTo<ExamineResult>();
			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<ExamineResultEditDto>();
		}

		/// <summary>
		/// 编辑ExamineResult
		/// </summary>
		
		protected virtual async Task Update(ExamineResultEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ExamineResult信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ExamineResult的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 根据ExamineDetailId获取信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ExamineResultListDto> GetExamineResultByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v=>v.ExamineDetailId == input.Id);
            return entity.MapTo<ExamineResultListDto>();
        }
    }
}



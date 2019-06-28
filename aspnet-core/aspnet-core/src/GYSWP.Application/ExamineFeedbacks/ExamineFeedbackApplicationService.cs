
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


using GYSWP.ExamineFeedbacks;
using GYSWP.ExamineFeedbacks.Dtos;
using GYSWP.ExamineFeedbacks.DomainService;
using GYSWP.Dtos;

namespace GYSWP.ExamineFeedbacks
{
    /// <summary>
    /// ExamineFeedback应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ExamineFeedbackAppService : GYSWPAppServiceBase, IExamineFeedbackAppService
    {
        private readonly IRepository<ExamineFeedback, Guid> _entityRepository;

        private readonly IExamineFeedbackManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExamineFeedbackAppService(
        IRepository<ExamineFeedback, Guid> entityRepository
        ,IExamineFeedbackManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取ExamineFeedback的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ExamineFeedbackListDto>> GetPaged(GetExamineFeedbacksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ExamineFeedbackListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ExamineFeedbackListDto>>();

			return new PagedResultDto<ExamineFeedbackListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ExamineFeedbackListDto信息
		/// </summary>
		 
		public async Task<ExamineFeedbackListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ExamineFeedbackListDto>();
		}

		/// <summary>
		/// 获取编辑 ExamineFeedback
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetExamineFeedbackForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetExamineFeedbackForEditOutput();
ExamineFeedbackEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ExamineFeedbackEditDto>();

				//examineFeedbackEditDto = ObjectMapper.Map<List<examineFeedbackEditDto>>(entity);
			}
			else
			{
				editDto = new ExamineFeedbackEditDto();
			}

			output.ExamineFeedback = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ExamineFeedback的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateExamineFeedbackInput input)
		{
			if (input.ExamineFeedback.Id.HasValue)
			{
				await Update(input.ExamineFeedback);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
			{
                var user = await GetCurrentUserAsync();
                input.ExamineFeedback.EmployeeId = user.EmployeeId;
                input.ExamineFeedback.EmployeeName = user.EmployeeName;
                var entity = await Create(input.ExamineFeedback);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
        }


		/// <summary>
		/// 新增ExamineFeedback
		/// </summary>
		
		protected virtual async Task<ExamineFeedbackEditDto> Create(ExamineFeedbackEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ExamineFeedback>(input);
            var entity=input.MapTo<ExamineFeedback>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<ExamineFeedbackEditDto>();
		}

		/// <summary>
		/// 编辑ExamineFeedback
		/// </summary>
		
		protected virtual async Task Update(ExamineFeedbackEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ExamineFeedback信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ExamineFeedback的方法
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
        public async Task<ExamineFeedbackListDto> GetExamineFeedbackByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.BusinessId == input.Id);
            return entity.MapTo<ExamineFeedbackListDto>();
        }
    }
}




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


using GYSWP.ExamineDetails;
using GYSWP.ExamineDetails.Dtos;
using GYSWP.ExamineDetails.DomainService;
using GYSWP.Clauses;
using GYSWP.Documents;
using GYSWP.Dtos;
using GYSWP.CriterionExamines;

namespace GYSWP.ExamineDetails
{
    /// <summary>
    /// ExamineDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ExamineDetailAppService : GYSWPAppServiceBase, IExamineDetailAppService
    {
        private readonly IRepository<ExamineDetail, Guid> _entityRepository;
        private readonly IRepository<Clause, Guid> _clauseRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IExamineDetailManager _entityManager;
        private readonly IRepository<CriterionExamine, Guid> _criterionExamineRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExamineDetailAppService(
        IRepository<ExamineDetail, Guid> entityRepository
        , IExamineDetailManager entityManager
        , IRepository<Clause, Guid> clauseRepository
        , IRepository<Document, Guid> documentRepository
        , IRepository<CriterionExamine, Guid> criterionExamineRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _clauseRepository = clauseRepository;
            _documentRepository = documentRepository;
            _criterionExamineRepository = criterionExamineRepository;
        }


        /// <summary>
        /// 获取ExamineDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ExamineDetailListDto>> GetPaged(GetExamineDetailsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ExamineDetailListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ExamineDetailListDto>>();

            return new PagedResultDto<ExamineDetailListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ExamineDetailListDto信息
        /// </summary>

        public async Task<ExamineDetailListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<ExamineDetailListDto>();
        }

        /// <summary>
        /// 获取编辑 ExamineDetail
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetExamineDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetExamineDetailForEditOutput();
            ExamineDetailEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ExamineDetailEditDto>();

                //examineDetailEditDto = ObjectMapper.Map<List<examineDetailEditDto>>(entity);
            }
            else
            {
                editDto = new ExamineDetailEditDto();
            }

            output.ExamineDetail = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改ExamineDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateExamineDetailInput input)
        {

            if (input.ExamineDetail.Id.HasValue)
            {
                await Update(input.ExamineDetail);
            }
            else
            {
                await Create(input.ExamineDetail);
            }
        }


        /// <summary>
        /// 新增ExamineDetail
        /// </summary>

        protected virtual async Task<ExamineDetailEditDto> Create(ExamineDetailEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ExamineDetail>(input);
            var entity = input.MapTo<ExamineDetail>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<ExamineDetailEditDto>();
        }

        /// <summary>
        /// 编辑ExamineDetail
        /// </summary>

        protected virtual async Task Update(ExamineDetailEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除ExamineDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ExamineDetail的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 根据id获取考核记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ExamineRecordDto>> GetExamineRecordByIdAsync(GetExamineDetailsInput input)
        {
            var query = _entityRepository.GetAll().Where(v => v.CriterionExamineId == input.ExamineId);
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            //var clause = _clauseRepository.GetAll().Select(v=>new {v.Id,v.ClauseNo,v.Title,v.Content });
            var clause = _clauseRepository.GetAll();
            var list = (from q in query
                        join d in doc on q.DocumentId equals d.Id
                        join c in clause on q.ClauseId equals c.Id
                        select new ExamineRecordDto()
                        {
                            Id = q.Id,
                            DocumentName = d.Name,
                            ClauseInfo = c.ClauseNo + "-" + (c.Title.Length > 15 ? c.Title.Substring(0, 15) + "..." : c.Title) + "-" + (c.Content.Length > 50 ? c.Content.Substring(0, 50) + "..." : c.Content),
                            Status = q.Status,
                            Result = q.Result,
                            EmployeeName = q.EmployeeName
                        });
            var count = await list.CountAsync();
            var entityList = await list.OrderBy(v => v.Status).ThenByDescending(v => v.Result).ThenBy(v => v.EmployeeName).ThenBy(v => v.DocumentName).PageBy(input).ToListAsync();
            return new PagedResultDto<ExamineRecordDto>(count, entityList);
        }

        /// <summary>
        /// 获取当前用户考核详情
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<ExamineRecordDto>> GetExamineDetailByCurrentIdAsync(GetExamineDetailsInput input)
        {
            var user = await GetCurrentUserAsync();
            var query = _entityRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId && v.CriterionExamineId == input.ExamineId);
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            var clause = _clauseRepository.GetAll();
            var list = (from q in query
                        join d in doc on q.DocumentId equals d.Id
                        join c in clause on q.ClauseId equals c.Id
                        select new ExamineRecordDto()
                        {
                            Id = q.Id,
                            DocumentName = d.Name,
                            ClauseInfo = c.ClauseNo + "-" + (c.Title.Length > 15 ? c.Title.Substring(0, 15) + "..." : c.Title) + "-" + (c.Content.Length > 50 ? c.Content.Substring(0, 50) + "..." : c.Content),
                            Status = q.Status,
                            Result = q.Result,
                            EmployeeName = q.EmployeeName
                        });
            var count = await list.CountAsync();
            var entityList = await list.OrderBy(v => v.Status).ThenByDescending(v=>v.Result).ThenBy(v => v.DocumentName).PageBy(input).ToListAsync();
            return new PagedResultDto<ExamineRecordDto>(count, entityList);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ExamineRecordDto> GetExamineDetailByIdAsync(GetExamineDetailsInput input)
        {
            var query = _entityRepository.GetAll().Where(v => v.Id == input.Id);
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            var clause = _clauseRepository.GetAll();
            var result = await (from q in query
                                join d in doc on q.DocumentId equals d.Id
                                join c in clause on q.ClauseId equals c.Id
                                select new ExamineRecordDto()
                                {
                                    Id = q.Id,
                                    DocumentName = d.Name,
                                    ClauseInfo = c.ClauseNo + "\t" + c.Title + "\r\n" + c.Content,
                                    Status = q.Status,
                                    Result = q.Result,
                                    EmployeeName = q.EmployeeName
                                }).FirstOrDefaultAsync();
            return result;
        }


        /// <summary>
        /// 修改检查状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ChangeStatusByIdAsync(GetExamineDetailsInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.Id == input.Id);
            entity.Result = input.Result;
            await _entityRepository.UpdateAsync(entity);
            return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        }

        /// <summary>
        /// 查看员工考核列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ExamineListDto>> GetExamineDetailByEmpIdAsync(GetExamineDetailsInput input)
        {
            var query = _entityRepository.GetAll().Where(v => v.EmployeeId == input.EmployeeId);
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            var criExamine = _criterionExamineRepository.GetAll();
            var list = (from q in query
                        join d in doc on q.DocumentId equals d.Id
                        join c in criExamine on q.CriterionExamineId equals c.Id
                        select new ExamineListDto()
                        {
                            Id = q.Id,
                            DocumentName = d.Name,
                            Status = q.Status,
                            Result = q.Result,
                            EmployeeName = q.EmployeeName,
                            Title = c.Title,
                            CreationTime = c.CreationTime,
                            CreatorDeptName =c.CreatorDeptName,
                            Type = c.Type,
                            DeptName = c.DeptName
                        });
            var count = await list.CountAsync();
            var entityList = await list.OrderByDescending(v => v.Status).ThenByDescending(v => v.Result).ThenBy(v => v.Title).ThenBy(v => v.DocumentName).PageBy(input).ToListAsync();
            return new PagedResultDto<ExamineListDto>(count, entityList);
        }
    }
}



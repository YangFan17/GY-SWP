
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


using GYSWP.IndicatorLibrarys;
using GYSWP.IndicatorLibrarys.Dtos;
using GYSWP.IndicatorLibrarys.DomainService;
using GYSWP.Documents;
using GYSWP.Employees;

namespace GYSWP.IndicatorLibrarys
{
    /// <summary>
    /// IndicatorLibrary应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IndicatorLibraryAppService : GYSWPAppServiceBase, IIndicatorLibraryAppService
    {
        private readonly IRepository<IndicatorLibrary, Guid> _entityRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IIndicatorLibraryManager _entityManager;
        private readonly IRepository<Employee, string> _employeeRepository;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorLibraryAppService(
        IRepository<IndicatorLibrary, Guid> entityRepository
        , IIndicatorLibraryManager entityManager
        , IRepository<Document, Guid> documentRepository
        , IRepository<Employee, string> employeeRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _documentRepository = documentRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 获取IndicatorLibrary的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<IndicatorLibraryListDto>> GetPaged(GetIndicatorLibrarysInput input)
        {
            var user = await GetCurrentUserAsync();
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var doc = _documentRepository.GetAll();
            var query = _entityRepository.GetAll().Where(v => v.Department == deptId).WhereIf(input.CycleTime.HasValue, v => v.CycleTime == input.CycleTime);
            var entityList = from q in query
                             join d in doc on q.SourceDocId equals d.Id
                             select new IndicatorLibraryListDto() {
                                 Id = q.Id,
                                 SourceDocName = d.Name,
                                 CycleTime = q.CycleTime,
                                 CreationTime = q.CreationTime,
                                 MeasuringWay = q.MeasuringWay,
                                 Paraphrase = q.Paraphrase,
                                 Title = q.Title
                             };
            var count = await entityList.CountAsync();
            var entityListDtos = await entityList
                    .OrderBy(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            return new PagedResultDto<IndicatorLibraryListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取IndicatorLibraryListDto信息
        /// </summary>

        public async Task<IndicatorLibraryListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<IndicatorLibraryListDto>();
        }

        /// <summary>
        /// 获取编辑 IndicatorLibrary
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetIndicatorLibraryForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetIndicatorLibraryForEditOutput();
            IndicatorLibraryEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<IndicatorLibraryEditDto>();

                //indicatorLibraryEditDto = ObjectMapper.Map<List<indicatorLibraryEditDto>>(entity);
            }
            else
            {
                editDto = new IndicatorLibraryEditDto();
            }

            output.IndicatorLibrary = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改IndicatorLibrary的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateIndicatorLibraryInput input)
        {

            if (input.IndicatorLibrary.Id.HasValue)
            {
                await Update(input.IndicatorLibrary);
            }
            else
            {
                await Create(input.IndicatorLibrary);
            }
        }


        /// <summary>
        /// 新增IndicatorLibrary
        /// </summary>

        protected virtual async Task<IndicatorLibraryEditDto> Create(IndicatorLibraryEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <IndicatorLibrary>(input);
            var entity = input.MapTo<IndicatorLibrary>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<IndicatorLibraryEditDto>();
        }

        /// <summary>
        /// 编辑IndicatorLibrary
        /// </summary>

        protected virtual async Task Update(IndicatorLibraryEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除IndicatorLibrary信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除IndicatorLibrary的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出IndicatorLibrary为excel表,等待开发。
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

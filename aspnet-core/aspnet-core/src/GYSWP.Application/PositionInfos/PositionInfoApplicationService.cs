
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


using GYSWP.PositionInfos;
using GYSWP.PositionInfos.Dtos;
using GYSWP.PositionInfos.DomainService;
using GYSWP.Dtos;
using GYSWP.MainPointsRecords;
using System.IO;
using System.Text.RegularExpressions;
using GYSWP.Employees;
using GYSWP.Documents;
using GYSWP.Categorys;

namespace GYSWP.PositionInfos
{
    /// <summary>
    /// PositionInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class PositionInfoAppService : GYSWPAppServiceBase, IPositionInfoAppService
    {
        private readonly IRepository<PositionInfo, Guid> _entityRepository;
        private readonly IRepository<MainPointsRecord, Guid> _mainPointsRecordRepository;
        private readonly IPositionInfoManager _entityManager;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<Category, int> _categoryRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public PositionInfoAppService(
        IRepository<PositionInfo, Guid> entityRepository
        , IPositionInfoManager entityManager
        , IRepository<MainPointsRecord, Guid> mainPointsRecordRepository
        , IRepository<Employee, string> employeeRepository
        , IRepository<Document, Guid> documentRepository
        , IRepository<Category, int> categoryRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _mainPointsRecordRepository = mainPointsRecordRepository;
            _employeeRepository = employeeRepository;
            _documentRepository = documentRepository;
            _categoryRepository = categoryRepository;
        }


        /// <summary>
        /// 获取PositionInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<PositionInfoListDto>> GetPaged(GetPositionInfosInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<PositionInfoListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<PositionInfoListDto>>();

            return new PagedResultDto<PositionInfoListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取PositionInfoListDto信息
        /// </summary>

        public async Task<PositionInfoListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<PositionInfoListDto>();
        }

        /// <summary>
        /// 获取编辑 PositionInfo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetPositionInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetPositionInfoForEditOutput();
            PositionInfoEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<PositionInfoEditDto>();

                //positionInfoEditDto = ObjectMapper.Map<List<positionInfoEditDto>>(entity);
            }
            else
            {
                editDto = new PositionInfoEditDto();
            }

            output.PositionInfo = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改PositionInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdatePositionInfoInput input)
        {

            if (input.PositionInfo.Id.HasValue)
            {
                await Update(input.PositionInfo);
            }
            else
            {
                await Create(input.PositionInfo);
            }
        }


        /// <summary>
        /// 新增PositionInfo
        /// </summary>

        protected virtual async Task<PositionInfoEditDto> Create(PositionInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <PositionInfo>(input);
            var entity = input.MapTo<PositionInfo>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<PositionInfoEditDto>();
        }

        /// <summary>
        /// 编辑PositionInfo
        /// </summary>

        protected virtual async Task Update(PositionInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除PositionInfo信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除PositionInfo的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 获取当前登录用户的PositionInfo分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<List<PosInfoListOut>> GetPositionListByCurrentUserAsync()
        {
            var user = await GetCurrentUserAsync();
            var query = from e in _entityRepository.GetAll()
                        .Where(i => i.EmployeeId == user.EmployeeId)
                        select new PosInfoListOut
                        {
                            Id = e.Id,
                            Duties = e.Duties
                        };
            var entityList = await query.ToListAsync();
            if (entityList.Count() <= 0)
            {
                return null;
            }
            return entityList;
        }

        public async Task<APIResultDto> CreatePositionInfoAsync(PosInfoInput input)
        {
            var user = await GetCurrentUserAsync();
            string position = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Position).FirstOrDefaultAsync();
            PositionInfo entity = new PositionInfo();
            entity.Duties = input.Duties;
            entity.EmployeeId = user.EmployeeId;
            entity.EmployeeName = user.EmployeeName;
            entity.Position = position;
            Guid id = await _entityRepository.InsertAndGetIdAsync(entity);
            return new APIResultDto
            {
                Code = 0,
                Data = id
            };
        }

        public async Task<List<HomeCategoryOption>> GetHomeCategoryOptionsAsync()
        {
            var curUser = await GetCurrentUserAsync();
            var deptId = await _employeeRepository.GetAll().Where(v => v.Id == curUser.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var zuofeiCategory = await _categoryRepository.GetAll().Where(v => "[" + v.DeptId + "]" == deptId && v.Name == "作废标准库").Select(v => new { v.Id }).FirstOrDefaultAsync();
            var entity = await (from c in _categoryRepository.GetAll().Where(v => "[" + v.DeptId + "]" == deptId && v.ParentId != 0 && v.ParentId != zuofeiCategory.Id)
                                select new HomeCategoryOption
                                {
                                    Id = c.Id,
                                    Title = c.Name
                                }).OrderBy(v => v.Id).ToListAsync();

            entity.ForEach(e => e.Children =
            (from d in _documentRepository
            .GetAll()
            .Where(d => d.CategoryId == e.Id)
             select new CategoryDocOption
             {
                 Id = d.Id,
                 Title = d.Name
             }).OrderBy(v => v.Id).ToList()
            );
            return entity;
        }


        /// <summary>
        /// 获取用户职位
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentPositionAsync()
        {
            var user = await GetCurrentUserAsync();
            string result = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Position).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// 获取要点记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<MainPointsList>> GetMainPointsChildrenAsync(Guid id)
        {
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name, v.DocNo });
            var points = _mainPointsRecordRepository.GetAll().Where(v => v.PositionInfoId == id);
            var pointsInfo = await (from po in points
                                    join d in doc on po.DocumentId equals d.Id
                                    select new MainPointsList()
                                    {
                                        DocName = d.Name,
                                        DocNo = d.DocNo,
                                        MainPoint = po.MainPoint,
                                        MainPointId = po.Id,
                                        DocId = d.Id
                                    }).ToListAsync();
            return pointsInfo;
        }

        /// <summary>
        /// 获取工作职责
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<HomePositionList>> GetPositionTreeByIdAsync()
        {
            var user = await GetCurrentUserAsync();
            var posList = _entityRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId);
            var list = await (from p in posList
                              select new HomePositionList()
                              {
                                  Id = p.Id,
                                  Duties = p.Duties,
                              }).ToListAsync();
            foreach (var item in list)
            {
                item.Children = await GetMainPointsChildrenAsync(item.Id);
            }
            return list;
        }

        #region 工作职责导入方法
        /// <summary>
        /// 工作职责批量导入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> PositionInfoImportAsync(string input)
        {
            try
            {
                var docs = GetPosInfoByDirectory($@"{input}");
                foreach (var doc in docs)
                {
                    string position = doc.Position.Split('.')[0];
                    var empList = await _employeeRepository.GetAll().Where(v => v.Position == position).Select(v => new { v.Id, v.Name }).ToListAsync();
                    if (empList.Count == 0)
                    {
                        return new APIResultDto() { Code = 666, Msg = "导入失败,不存在员工" };
                    }
                    foreach (var emp in empList)
                    {
                        foreach (var posItem in doc.PosInfo)
                        {
                            PositionInfo pos = new PositionInfo();
                            pos.Duties = posItem.Duties;
                            pos.Position = position;
                            pos.EmployeeId = emp.Id;
                            pos.EmployeeName = emp.Name;
                            Guid posId = await _entityRepository.InsertAndGetIdAsync(pos);
                            await CurrentUnitOfWork.SaveChangesAsync();
                            foreach (var detailItem in posItem.Sections)
                            {
                                Guid docId = await _documentRepository.GetAll().Where(v => v.Name == detailItem.DocName).Select(v => v.Id).FirstOrDefaultAsync();
                                if (docId == Guid.Empty)
                                {
                                    return new APIResultDto() { Code = 888, Msg = "导入失败,不存在文档" };
                                }
                                MainPointsRecord point = new MainPointsRecord();
                                point.PositionInfoId = posId;
                                point.MainPoint = detailItem.Context;
                                point.DocumentId = docId;
                                await _mainPointsRecordRepository.InsertAsync(point);
                            }
                        }
                    }
                }

                return new APIResultDto() { Code = 0, Msg = "导入成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DocumentReadAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 999, Msg = "导入失败" };
            }

        }

        /// <summary>
        /// 工作职责批量读取
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private static List<DocInfo> GetPosInfoByDirectory(string directoryPath)
        {
            var docs = new List<DocInfo>();
            DirectoryInfo di = new DirectoryInfo(directoryPath);
            FileInfo[] fis = di.GetFiles();
            foreach (var fi in fis)
            {
                var doc = new DocInfo();
                doc.Position = fi.Name;

                string data = string.Empty;
                using (StreamReader sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.GetEncoding("GB2312")))
                {
                    data = sr.ReadToEnd();
                }
                string[] brandData = data.Split("\r\n");
                int index = 1;
                foreach (string b in brandData)
                {
                    var line = b.Trim();
                    if (line.Length > 0)
                    {
                        if (index == 1)//找到第一行
                        {
                            var positionInfo = new PosInfo();
                            positionInfo.Duties = line;
                            doc.PosInfo.Add(positionInfo);
                            index++;
                        }
                        else if (index > 1)
                        {
                            bool isNext = Regex.IsMatch(line, @"\d+");
                            var sectionInfo = new SectionInfo();
                            if (line.Contains("\t"))
                            {
                                bool isDoc = Regex.IsMatch(line.Split("Q")[0], @"\d+");
                                if (isDoc == false)
                                {
                                    sectionInfo.DocName = line.Split("\t")[0];
                                    doc.PosInfo.Last().Sections.Add(sectionInfo);
                                }
                            }
                            else if (!line.Contains("\t") && isNext == true)
                            {
                                doc.PosInfo.Last().Sections.Last().Context += (line + "\r\n");
                            }
                            else
                            {
                                var positionInfo = new PosInfo();
                                positionInfo.Duties = line;
                                doc.PosInfo.Add(positionInfo);
                            }
                        }
                    }
                }

                docs.Add(doc);
            }
            return docs;
        }
        #endregion
    }
    #region 岗位职责导入类
    public class DocInfo
    {
        public DocInfo()
        {
            PosInfo = new List<PosInfo>();
        }
        public string Position { get; set; }
        public List<PosInfo> PosInfo { get; set; }
    }
    public class PosInfo
    {
        public PosInfo()
        {
            Sections = new List<SectionInfo>();
        }
        public string Duties { get; set; }

        public List<SectionInfo> Sections { get; set; }
    }

    public class SectionInfo
    {
        public string DocName { get; set; }
        public string Context { get; set; }
    }
    #endregion
}
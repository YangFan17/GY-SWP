
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


using GYSWP.DocAttachments;
using GYSWP.DocAttachments.Dtos;
using GYSWP.DocAttachments.DomainService;
using GYSWP.Dtos;

namespace GYSWP.DocAttachments
{
    /// <summary>
    /// LC_Attachment应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_AttachmentAppService : GYSWPAppServiceBase, ILC_AttachmentAppService
    {
        private readonly IRepository<LC_Attachment, Guid> _entityRepository;

        private readonly ILC_AttachmentManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_AttachmentAppService(
        IRepository<LC_Attachment, Guid> entityRepository,
        IRepository<LC_Attachment, Guid> AttachRepository
        , ILC_AttachmentManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _entityRepository = AttachRepository;
        }


        /// <summary>
        /// 获取LC_Attachment的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_AttachmentListDto>> GetPaged(GetLC_AttachmentsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<LC_AttachmentListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<LC_AttachmentListDto>>();

            return new PagedResultDto<LC_AttachmentListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_AttachmentListDto信息
        /// </summary>

        public async Task<LC_AttachmentListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_AttachmentListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_Attachment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_AttachmentForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_AttachmentForEditOutput();
            LC_AttachmentEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_AttachmentEditDto>();

                //lC_AttachmentEditDto = ObjectMapper.Map<List<lC_AttachmentEditDto>>(entity);
            }
            else
            {
                editDto = new LC_AttachmentEditDto();
            }

            output.LC_Attachment = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_Attachment的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateLC_AttachmentInput input)
        {

            if (input.LC_Attachment.Id.HasValue)
            {
                await Update(input.LC_Attachment);
            }
            else
            {
                await Create(input.LC_Attachment);
            }
        }


        /// <summary>
        /// 新增LC_Attachment
        /// </summary>
        protected virtual async Task<LC_AttachmentEditDto> Create(LC_AttachmentEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_Attachment>(input);
            var entity = input.MapTo<LC_Attachment>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_AttachmentEditDto>();
        }

        /// <summary>
        /// 编辑LC_Attachment
        /// </summary>

        protected virtual async Task Update(LC_AttachmentEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LC_Attachment信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_Attachment的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 钉钉提交拍照信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateAttachmentAsync(DingDingAttachmentEditDto input)
        {
            foreach (var item in input.Path)
            {
                var entity = new LC_Attachment();
                entity.Path = item;
                entity.EmployeeId = input.EmployeeId;
                entity.Type = input.Type;
                entity.Remark = input.Remark;
                await _entityRepository.InsertAsync(entity);
            }
            return new APIResultDto() { Code = 0, Msg = "上传成功" };
        }
    }
}
﻿
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Abp.Domain.Repositories;
using GYSWP.Dtos;
using GYSWP.DingDingApproval.Dtos;
using Senparc.CO2NET.Helpers;
using System.IO;
using System.Text;
using Senparc.CO2NET.HttpUtility;
using System.Collections;
using Newtonsoft.Json;
using GYSWP.SystemDatas;
using GYSWP.Organizations;
using GYSWP.Employees;
using GYSWP.DingDing.Dtos;
using GYSWP.DingDing;
using GYSWP.ClauseRevisions;
using GYSWP.Clauses;
using GYSWP.GYEnums;
using GYSWP.ApplyInfos;
using GYSWP.DocRevisions;
using GYSWP.Advises;
using GYSWP.Advises.Dtos;

namespace GYSWP.DingDingApproval
{
    [AbpAuthorize]
    public class ApprovalAppService : GYSWPAppServiceBase, IApprovalAppService
    {
        private readonly IRepository<SystemData> _systemDataRepository;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<ClauseRevision, Guid> _clauseRevisionRepository;
        private readonly IRepository<Clause, Guid> _clauseRepository;
        private readonly IDingDingAppService _dingDingAppService;
        private readonly IRepository<ApplyInfo, Guid> _applyInfoRepository;
        private readonly IRepository<DocRevision, Guid> _docRevisionRepository;
        private readonly IRepository<Advise, Guid> _adviceRepository;

        public ApprovalAppService(IRepository<SystemData> systemDataRepository
            , IRepository<Organization, long> organizationRepository
            , IRepository<Employee, string> employeeRepository
            , IDingDingAppService dingDingAppService
            , IRepository<ClauseRevision, Guid> clauseRevisionRepository
            , IRepository<Clause, Guid> clauseRepository
            , IRepository<ApplyInfo, Guid> applyInfoRepository
            , IRepository<DocRevision, Guid> docRevisionRepository
            , IRepository<Advise, Guid> adviceRepository
)
        {
            _systemDataRepository = systemDataRepository;
            _organizationRepository = organizationRepository;
            _employeeRepository = employeeRepository;
            _dingDingAppService = dingDingAppService;
            _clauseRevisionRepository = clauseRevisionRepository;
            _clauseRepository = clauseRepository;
            _applyInfoRepository = applyInfoRepository;
            _docRevisionRepository = docRevisionRepository;
            _adviceRepository = adviceRepository;
        }

        /// <summary>
        /// 发起制修订申请
        /// </summary>
        /// <param name="Reason"></param>
        /// <param name="Content"></param>
        /// <param name="CreationTime"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> SubmitDocApproval(string Reason, string Content, DateTime CreationTime, OperateType OperateType, string DocName)
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var user = await GetCurrentUserAsync();
            var dept = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = dept.Replace('[', ' ').Replace(']', ' ').Trim();
            var url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/create?access_token={0}", accessToken);
            SubmitApprovalEntity request = new SubmitApprovalEntity();
            //发布需要更改路径
            //request.process_code = "PROC-C3F82626-4DBB-4A6D-8EF1-517D3892CEBB";//202
            request.process_code = "PROC-6DF666D7-32CC-49F0-B9F3-1FF4D87D8635";
            request.originator_user_id = user.EmployeeId;
            request.agent_id = ddConfig.AgentID;
            request.dept_id = Convert.ToInt32(deptId);
            List<Approval> approvalList = new List<Approval>();
            approvalList.Add(new Approval() { name = "申请类型", value = OperateType.ToString() });
            approvalList.Add(new Approval() { name = "标准名称", value = DocName });
            approvalList.Add(new Approval() { name = "申请原因", value = Reason });
            approvalList.Add(new Approval() { name = "申请内容", value = Content });
            approvalList.Add(new Approval() { name = "申请人", value = user.EmployeeName });
            approvalList.Add(new Approval() { name = "申请时间", value = CreationTime.ToString("yyyy-MM-dd HH:mm") });
            request.form_component_values = approvalList;
            ApprovalReturn approvalReturn = new ApprovalReturn();
            var jsonString = SerializerHelper.GetJsonString(request, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                approvalReturn = Post.PostGetJson<ApprovalReturn>(url, null, ms);
            };
            if (approvalReturn.errcode == 0)
            {
                return new APIResultDto() { Code = 0, Msg = "提交成功", Data = approvalReturn.process_instance_id };
            }
            else
            {
                return new APIResultDto() { Code = 4, Msg = "提交失败", Data = approvalReturn.errmsg };
            }
        }

        /// <summary>
        /// 提交修订审批流程
        /// </summary>
        /// <param name="ApplyInfoId"></param>
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> SubmitRevisionApproval(Guid ApplyInfoId, Guid DocumentId)
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var user = await GetCurrentUserAsync();
            var dept = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = dept.Replace('[', ' ').Replace(']', ' ').Trim();
            var url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/create?access_token={0}", accessToken);
            string pId = await _applyInfoRepository.GetAll().Where(v => v.Id == ApplyInfoId).Select(v => v.ProcessInstanceId).FirstOrDefaultAsync();
            var clauseList = await _clauseRevisionRepository.GetAll().Where(v => v.DocumentId == DocumentId && v.ApplyInfoId == ApplyInfoId).OrderBy(v => v.RevisionType).ThenBy(v => v.ClauseNo).ThenBy(v => v.CreationTime).ToListAsync();
            int Cnumber = await _clauseRevisionRepository.CountAsync(v => v.DocumentId == DocumentId && v.ApplyInfoId == ApplyInfoId && v.RevisionType == GYEnums.RevisionType.新增);
            int Unumber = await _clauseRevisionRepository.CountAsync(v => v.DocumentId == DocumentId && v.ApplyInfoId == ApplyInfoId && v.RevisionType == GYEnums.RevisionType.修订);
            int Dnumber = await _clauseRevisionRepository.CountAsync(v => v.DocumentId == DocumentId && v.ApplyInfoId == ApplyInfoId && v.RevisionType == GYEnums.RevisionType.删除);
            int Total = Cnumber + Unumber + Dnumber;
            SubmitApprovalEntity request = new SubmitApprovalEntity();
            //发布需要更改路径
            //request.process_code = "PROC-34E6C3FC-B1B7-4569-9DEC-1B2E7EA0E1A1";//202
            request.process_code = "PROC-051C520B-499E-4A94-947A-B5FFFC47750C";
            request.originator_user_id = user.EmployeeId;
            request.agent_id = ddConfig.AgentID;
            request.dept_id = Convert.ToInt32(deptId);
            List<Approval> approvalList = new List<Approval>();
            approvalList.Add(new Approval() { name = "申请人", value = user.EmployeeName });
            approvalList.Add(new Approval() { name = "申请时间", value = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
            approvalList.Add(new Approval() { name = "条款汇总", value = string.Format("共 {0} 条，包括：新增 {1}， 修订 {2} ，删除 {3}", Total, Cnumber, Unumber, Dnumber) });
            ArrayList items = new ArrayList();
            foreach (var item in clauseList)
            {
                var clause = await _clauseRepository.FirstOrDefaultAsync(v => v.Id == item.ClauseId);
                ArrayList revisionDetail = new ArrayList();
                revisionDetail.Add(new Approval() { name = "操作状态", value = item.RevisionType.ToString() });
                revisionDetail.Add(new Approval() { name = "提交时间", value = item.CreationTime.ToString("yyyy-MM-dd HH:mm") });
                if (item.RevisionType == GYEnums.RevisionType.新增)
                {
                    revisionDetail.Add(new Approval()
                    {
                        name = "原始内容",
                        value = "无"
                    });
                    revisionDetail.Add(new Approval()
                    {
                        name = "当前内容",
                        //value = "[编号]：" + item.ClauseNo + "\n"
                        //+ "[标题]：" + (item.Title.Length > 10 ? item.Title.Substring(0, 10) + "..." : item.Title) + "\n"
                        //+ "[内容]：" + (item.Content.Length > 30 ? item.Content.Substring(0, 30) + "..." : item.Content)
                        //                    value = "[编号]：" + item.ClauseNo + "\n"
                        //+ "[标题]：" + item.Title + "\n"
                        //+ "[内容]：" + item.Content
                        value = item.ClauseNo + (item.Title != null ? ("\t" + item.Title) : "")
                        + (item.Content != null ? ("\r\n" + item.Content) : "")
                    });
                }
                else if (item.RevisionType == GYEnums.RevisionType.修订)
                {
                    revisionDetail.Add(new Approval()
                    {
                        name = "原始内容",
                        //                    value = "[编号]：" + clause.ClauseNo + "\n"
                        //+ "[标题]：" + (clause.Title.Length > 10 ? clause.Title.Substring(0, 10) + "..." : clause.Title) + "\n"
                        //+ "[内容]：" + (clause.Content.Length > 30 ? clause.Content.Substring(0, 30) + "..." : clause.Content)
                        //                    value = "[编号]：" + clause.ClauseNo + "\n"
                        //+ "[标题]：" + clause.Title + "\n"
                        //+ "[内容]：" + clause.Content
                        value = clause.ClauseNo + (clause.Title != null ? ("\t" + clause.Title) : "")
                        + (clause.Content != null ? ("\r\n" + clause.Content) : "")
                    });
                    revisionDetail.Add(new Approval()
                    {
                        name = "当前内容",
                        //value = "[编号]：" + item.ClauseNo + "\n"
                        //+ "[标题]：" + (item.Title.Length > 10 ? item.Title.Substring(0, 10) + "..." : item.Title) + "\n"
                        //+ "[内容]：" + (item.Content.Length > 30 ? item.Content.Substring(0, 30) + "..." : item.Content)
                        //value = "[编号]：" + item.ClauseNo + "\n"
                        //+ "[标题]：" + item.Title + "\n"
                        //+ "[内容]：" + item.Content
                        value = item.ClauseNo + (item.Title != null ? ("\t" + item.Title) : "")
                        + (item.Content != null ? ("\r\n" + item.Content) : "")
                    });
                }
                else
                {
                    revisionDetail.Add(new Approval()
                    {
                        name = "原始内容",
                        //                    value = "[编号]：" + clause.ClauseNo + "\n"
                        //+ "[标题]：" + (clause.Title.Length > 10 ? clause.Title.Substring(0, 10) + "..." : clause.Title) + "\n"
                        //+ "[内容]：" + (clause.Content.Length > 30 ? clause.Content.Substring(0, 30) + "..." : clause.Content)
                        //                });
                        //                    value = "[编号]：" + clause.ClauseNo + "\n"
                        //+ "[标题]：" + clause.Title + "\n"
                        //+ "[内容]：" + clause.Content
                        value = clause.ClauseNo + (clause.Title != null ? ("\t" + clause.Title) : "")
                        + (clause.Content != null ? ("\r\n" + clause.Content) : "")
                    });
                    revisionDetail.Add(new Approval()
                    {
                        name = "当前内容",
                        value = "无"
                    });
                }

                items.Add(revisionDetail);
            }
            approvalList.Add(new Approval() { name = "制修订明细", value = JsonConvert.SerializeObject(items) });
            approvalList.Add(new Approval() { name = "关联审批", value = string.Format("[\"{0}\"]", pId) });
            request.form_component_values = approvalList;
            ApprovalReturn approvalReturn = new ApprovalReturn();
            var jsonString = SerializerHelper.GetJsonString(request, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                approvalReturn = Post.PostGetJson<ApprovalReturn>(url, null, ms);
            };
            if (approvalReturn.errcode == 0)
            {
                return new APIResultDto() { Code = 0, Msg = "提交成功", Data = approvalReturn.process_instance_id };
            }
            else
            {
                return new APIResultDto() { Code = 4, Msg = "提交失败", Data = approvalReturn.errmsg };
            }
        }


        /// <summary>
        /// 提交制定标准审批流程
        /// </summary>
        /// <param name="ApplyInfoId"></param>
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> SubmitDraftDocApproval(Guid ApplyInfoId, Guid DocumentId)
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var user = await GetCurrentUserAsync();
            var dept = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = dept.Replace('[', ' ').Replace(']', ' ').Trim();
            var url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/create?access_token={0}", accessToken);
            string pId = await _applyInfoRepository.GetAll().Where(v => v.Id == ApplyInfoId).Select(v => v.ProcessInstanceId).FirstOrDefaultAsync();
            string docName = await _docRevisionRepository.GetAll().Where(v => v.Id == DocumentId).Select(v => v.Name).FirstOrDefaultAsync();
            var clauseList = await _clauseRevisionRepository.GetAll().Where(v => v.DocumentId == DocumentId && v.RevisionType == RevisionType.标准制定 && v.ApplyInfoId == ApplyInfoId).OrderBy(v => v.RevisionType).ThenBy(v => v.ClauseNo).ThenByDescending(v => v.CreationTime).ToListAsync();
            SubmitApprovalEntity request = new SubmitApprovalEntity();
            //发布需要更改路径
            //request.process_code = "PROC-BFE69EF9-4B66-4697-B917-362D28B71F68";//202
            request.process_code = "PROC-DCD404D8-883A-4481-9D76-31FA40F03A71";
            request.originator_user_id = user.EmployeeId;
            request.agent_id = ddConfig.AgentID;
            request.dept_id = Convert.ToInt32(deptId);
            //request.dept_id = 67209026; // 测试环境 发布需要放开
            List<Approval> approvalList = new List<Approval>();
            approvalList.Add(new Approval() { name = "申请人", value = user.EmployeeName });
            approvalList.Add(new Approval() { name = "申请时间", value = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
            approvalList.Add(new Approval() { name = "标准名称", value = docName });
            ArrayList items = new ArrayList();
            foreach (var item in clauseList)
            {
                var clause = await _clauseRepository.FirstOrDefaultAsync(v => v.Id == item.ClauseId);
                ArrayList revisionDetail = new ArrayList();
                revisionDetail.Add(new Approval()
                {
                    name = "制定内容",
                    //value = "[编号]：" + item.ClauseNo + "\n"
                    //+ "[标题]：" + (item.Title.Length > 10 ? item.Title.Substring(0, 10) + "..." : item.Title) + "\n"
                    //+ "[内容]：" + (item.Content.Length > 30 ? item.Content.Substring(0, 30) + "..." : item.Content)
                    //value = "[编号]：" + item.ClauseNo + "\n"
                    //+ "[标题]：" + item.Title + "\n"
                    //+ "[内容]：" + item.Content
                    value = item.ClauseNo + (item.Title != null ? ("\t" + item.Title) : "")
                        + (item.Content != null ? ("\r\n" + item.Content) : "")
                });
                items.Add(revisionDetail);
            }
            approvalList.Add(new Approval() { name = "标准制定明细", value = JsonConvert.SerializeObject(items) });
            approvalList.Add(new Approval() { name = "关联审批", value = string.Format("[\"{0}\"]", pId) });
            request.form_component_values = approvalList;
            ApprovalReturn approvalReturn = new ApprovalReturn();
            var jsonString = SerializerHelper.GetJsonString(request, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                approvalReturn = Post.PostGetJson<ApprovalReturn>(url, null, ms);
            };
            if (approvalReturn.errcode == 0)
            {
                return new APIResultDto() { Code = 0, Msg = "提交成功", Data = approvalReturn.process_instance_id };
            }
            else
            {
                return new APIResultDto() { Code = 4, Msg = "提交失败", Data = approvalReturn.errmsg };
            }
        }

        /// <summary>
        /// 提交意见反馈申请
        /// </summary>
        /// <param name="AdviseName"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="CreationTime"></param>
        /// <param name="CurrentSituation"></param>
        /// <param name="Solution"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> SubmitAdviceApproval(Guid id, List<FileData> fileDatas)
        {
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var advice = await _adviceRepository.GetAsync(id);
            //获取审批钉盘Id
            //var spaceInfo = GetProcessinstanceSpace(accessToken, advice.EmployeeId);
            var dept = await _employeeRepository.GetAll().Where(v => v.Id == advice.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = dept.Replace('[', ' ').Replace(']', ' ').Trim();
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id.ToString() == deptId).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/create?access_token={0}", accessToken);
            SubmitApprovalEntity request = new SubmitApprovalEntity();
            //request.process_code = "PROC-A0FDBDA5-BBC6-4004-B72B-88AFC92DC427";//202
            request.process_code = "PROC-LCZJIVQV-T66UAC9MNECX6VLI1U0S1-D2T6MRFJ-6";
            request.originator_user_id = advice.EmployeeId;
            request.agent_id = ddConfig.AgentID;
            request.dept_id = Convert.ToInt32(deptId);
            List<Approval> approvalList = new List<Approval>();
            approvalList.Add(new Approval() { name = "部门（单位）", value = deptName });
            approvalList.Add(new Approval() { name = "建议人", value = advice.EmployeeName });
            approvalList.Add(new Approval() { name = "联合建议人", value = advice.UnionEmpName == null ? "":advice.UnionEmpName });
            approvalList.Add(new Approval() { name = "申报日期", value = advice.CreationTime.ToString("yyyy-MM-dd HH:mm") });
            approvalList.Add(new Approval() { name = "建议名称", value = advice.AdviseName });
            approvalList.Add(new Approval() { name = "现状描述", value = advice.CurrentSituation });
            approvalList.Add(new Approval() { name = "对策建议", value = advice.Solution });
            approvalList.Add(new Approval() { name = "主要领导审批", value = advice.IsMainLeader == true ? "是" : "否" });
            approvalList.Add(new Approval() { name = "附件", value = SerializerHelper.GetJsonString(fileDatas, null) });
            request.form_component_values = approvalList;
            ApprovalReturn approvalReturn = new ApprovalReturn();
            var jsonString = SerializerHelper.GetJsonString(request, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                approvalReturn = Post.PostGetJson<ApprovalReturn>(url, null, ms);
            };
            if (approvalReturn.errcode == 0)
            {
                return new APIResultDto() { Code = 0, Msg = "提交成功", Data = approvalReturn.process_instance_id };
            }
            else
            {
                Logger.InfoFormat($"合理化建议审批异常：[{approvalReturn.errcode}]{approvalReturn.errmsg}");
                return new APIResultDto() { Code = 4, Msg = "提交失败", Data = approvalReturn.errmsg };
            }
        }

        /// <summary>
        /// 发送制定标准（企管编号盖章）钉钉工作通知
        /// </summary>
        [AbpAllowAnonymous]
        public APIResultDto SendMessageToQGAdminAsync(string docName, Guid docId)
        {
            try
            {
                //获取消息模板配置
                //string messageTitle = "您有新的意见反馈";
                //string messageMediaId = await _systemDataRepository.GetAll().Where(v => v.ModelId == ConfigModel.钉钉配置 && v.Type == ConfigType.标准化工作平台 && v.Code == GYCode.DocMediaId).Select(v => v.Desc).FirstOrDefaultAsync();
                DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var msgdto = new DingMsgDto();
                //msgdto.userid_list = "1926112826844702";//杨帆
                msgdto.userid_list = "166859002437613171";//企管科
                msgdto.to_all_user = false;
                msgdto.agent_id = ddConfig.AgentID;
                msgdto.msg.msgtype = "link";
                msgdto.msg.link.title = "您有新制定的标准需要编号";
                msgdto.msg.link.picUrl = "@lALPDeC2t6v4RPJAQA";
                msgdto.msg.link.text = $"您有新制定的标准需要编号[{ docName}] " + DateTime.Now.ToString();
                msgdto.msg.link.messageUrl = "eapp://page/document/document-approval?id=" + docId;
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", accessToken);
                var jsonString = SerializerHelper.GetJsonString(msgdto, null);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = Post.PostGetJson<object>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 发送（标准化管理员）文档更新提醒通知
        /// </summary>
        [AbpAllowAnonymous]
        public APIResultDto SendMessageToStandardAdminAsync(string docName, string empId)
        {
            try
            {
                //获取消息模板配置
                //string messageTitle = "您有新的意见反馈";
                //string messageMediaId = await _systemDataRepository.GetAll().Where(v => v.ModelId == ConfigModel.钉钉配置 && v.Type == ConfigType.标准化工作平台 && v.Code == GYCode.DocMediaId).Select(v => v.Desc).FirstOrDefaultAsync();
                DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var msgdto = new DingMsgDto();
                msgdto.userid_list = empId;
                msgdto.to_all_user = false;
                msgdto.agent_id = ddConfig.AgentID;
                msgdto.msg.msgtype = "text";
                msgdto.msg.text.content = $"您修订的[{ docName}] 标准审批已通过，请及时更新标准文档附件{DateTime.Now.ToString()}";
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", accessToken);
                var jsonString = SerializerHelper.GetJsonString(msgdto, null);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = Post.PostGetJson<object>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 发送指标考核填写通知[已停用]
        /// </summary>
        [AbpAllowAnonymous]
        public APIResultDto SendIndicatorMessageAsync(string empId)
        {
            try
            {
                DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var msgdto = new DingMsgDto();
                //msgdto.userid_list = empId; 
                msgdto.userid_list = "1926112826844702";
                msgdto.to_all_user = false;
                msgdto.agent_id = ddConfig.AgentID;
                msgdto.msg.msgtype = "text";
                msgdto.msg.text.content = $"您有新的目标指标检查下达，请前往标准化工作平台进行查看{DateTime.Now.ToString()}";
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", accessToken);
                var jsonString = SerializerHelper.GetJsonString(msgdto, null);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = Post.PostGetJson<object>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendIndicatorMessageAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 发送指标考核结果通知
        /// </summary>
        [AbpAllowAnonymous]
        public APIResultDto SendIndicatorResultAsync(IndicatorStatus status, string empList)
        {
            try
            {
                DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var msgdto = new DingMsgDto();
                msgdto.userid_list = empList;
                //msgdto.userid_list = "1926112826844702";
                msgdto.to_all_user = false;
                msgdto.agent_id = ddConfig.AgentID;
                msgdto.msg.msgtype = "text";
                msgdto.msg.text.content = $"您的目标指标检查{status.ToString()}，请前往标准化工作平台进行查看{DateTime.Now.ToString()}";
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", accessToken);
                var jsonString = SerializerHelper.GetJsonString(msgdto, null);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = Post.PostGetJson<object>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendIndicatorResultAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 发送条款检查填写通知
        /// </summary>
        [AbpAllowAnonymous]
        public APIResultDto SendCriterionExamineMessageAsync(string empId)
        {
            try
            {
                DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var msgdto = new DingMsgDto();
                msgdto.userid_list = empId;
                //msgdto.userid_list = "1926112826844702";
                msgdto.to_all_user = false;
                msgdto.agent_id = ddConfig.AgentID;
                msgdto.msg.msgtype = "text";
                msgdto.msg.text.content = $"您有新的标准条款检查下达，请前往标准化工作平台进行填写{DateTime.Now.ToString()}";
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", accessToken);
                var jsonString = SerializerHelper.GetJsonString(msgdto, null);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = Post.PostGetJson<object>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendCriterionExamineMessageAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 获取审批钉盘SpaceId
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="empId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public APIResultDto GetProcessinstanceSpace(string accessToken, string empId)
        {
            try
            {
                var url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/cspace/info?access_token={0}&user_id={1}", accessToken,empId);
                //var jsonString = SerializerHelper.GetJsonString(user_id, null);
                DingSpaceInfo obj = new DingSpaceInfo();
                using (MemoryStream ms = new MemoryStream())
                {
                    //var bytes = Encoding.UTF8.GetBytes(jsonString);
                    //ms.Write(null, 0, 0);
                    //ms.Seek(0, SeekOrigin.Begin);
                    obj = Post.PostGetJson<DingSpaceInfo>(url, null, ms);
                };
                if (obj.success == true)
                {
                    return new APIResultDto() { Code = 0, Data = obj.result.space_id };
                }
                else
                {
                    return new APIResultDto() { Code = 999, Msg = "获取审批钉盘空间接口失败" };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendCriterionExamineMessageAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "获取审批钉盘空间接口异常" };
            }

        }
        /// <summary> 
        /// 上传图片并返回MeadiaId
        /// </summary>
        //public object UpdateAndGetAdviseMediaId(string path)
        //{
        //    IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/media/upload");
        //    OapiMediaUploadRequest request = new OapiMediaUploadRequest();
        //    request.Type = "image";
        //    request.Media = new Top.Api.Util.FileItem($@"{path}");
        //    DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
        //    string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
        //    OapiMediaUploadResponse response = client.Execute(request, accessToken);
        //    return response;
        //}
    }
}

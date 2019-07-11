﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing.Dtos
{
    public class DingMsgDto
    {
        public DingMsgDto()
        {
            msg = new DingMsg();
        }
        public int agent_id { get; set; }

        public string userid_list { get; set; }

        public string dept_id_list { get; set; }

        public bool to_all_user { get; set; }

        public DingMsg msg { get; set; }
    }

    public class DingMsg
    {
        public DingMsg()
        {
            link = new DingLinkMsg();
            text = new DingTextMsg();
        }

        public string msgtype { get; set; }

        public DingLinkMsg link { get; set; }
        public DingTextMsg text { get; set; }

    }
    public class DingLinkMsg
    {
        public string messageUrl { get; set; }

        public string picUrl { get; set; }

        public string text { get; set; }

        public string title { get; set; }
    }
    public class DingTextMsg
    {
        public string content { get; set; }
    }

    public class Processinstance
    {
        public long? AgentId { get; set; }

        public long? DeptId { get; set; }

        public string ProcessCode { get; set; }

        public string OriginatorUserId { get; set; }
        public List<FormComponentValueVoDomain> FormComponentValues_ = new List<FormComponentValueVoDomain>();

    }

    [Serializable]

    public class FormComponentValueVoDomain
    {
        public string Name { get; set; }

        /// <summary>
        /// 表单每一栏的值
        /// </summary>
        public string Value { get; set; }
    }
}

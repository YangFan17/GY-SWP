using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_SortingEquipChecks.Dtos
{
    [AutoMapFrom(typeof(LC_SortingEquipCheck))]
    public class LC_SortingEquipCheckDto: LC_SortingEquipCheckEditDto
    {
        //public string EmployeeId { get; set; }
        public LC_AttachmentType Type { get; set; }
        public string[] Path { get; set; }
        public string Remark { get; set; }

        public Guid? BLL { get; set; }
    }
}




using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Clauses;
using System.Collections.Generic;
using Abp.AutoMapper;

namespace GYSWP.PositionInfos.Dtos
{
    [AutoMapFrom(typeof(PositionInfo))]
    public class PositionInfoListDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Position
        /// </summary>
        [Required(ErrorMessage = "Position不能为空")]
        public string Position { get; set; }



        /// <summary>
        /// Duties
        /// </summary>
        public string Duties { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }



        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; }
    }
    public class PositionInfoTreeNodeDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Duties { get; set; }
        public List<PositionInfoTreeNodeDto> Children = new List<PositionInfoTreeNodeDto>();
    }

    public class PosInfoListOut
    {
        public Guid Id { get; set; }
        public string Duties { get; set; }
    }

    public class HomeCategoryOption
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public bool IsEmpty { get; set; }

        public List<CategoryDocOption> Children { get; set; }
    }

    public class CategoryDocOption
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
    
    public class PositionInfoTreeListDto
    {
        public Guid Id { get; set; }
        public string Duties { get; set; }
    }

    public class HomePositionList
    {
        public HomePositionList()
        {
            Children = new List<MainPointsList>();
        }
        public Guid Id { get; set; }
        public string Duties { get; set; }
        public List<MainPointsList> Children { get; set; }
    }

    public class MainPointsList
    {
        public Guid DocId { get; set; }
        public string MainPoint { get; set; }
        public string DocName { get; set; }
        public string DocNo { get; set; }
        public Guid MainPointId { get; set; }
    }
    public class HomePositionTreeList
    {
        public HomePositionTreeList()
        {
            Children = new List<HomePositionTreeList>();
        }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        //public string MainPoint { get; set; }
        public string DocName { get; set; }
        public string DocNo { get; set; }
        public string Duties { get; set; }
        public string MainPoint { get; set; }
        public List<HomePositionTreeList> Children { get; set; }
    }
}
using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.Employees
{
    [Table("Employees")]
    public class Employee : Entity<string> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 当前应用内唯一标识
        /// </summary>
        [StringLength(200)]
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(11)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(100)]
        public virtual string Email { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool? Active { get; set; }
        /// <summary>
        /// 是否为企业管理员
        /// </summary>
        public virtual bool? IsAdmin { get; set; }
        /// <summary>
        /// 是否为老板
        /// </summary>
        public virtual bool? IsBoss { get; set; }
        /// <summary>
        /// 所属部门列表
        /// </summary>
        [StringLength(300)]
        public virtual string Department { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(100)]
        public virtual string Position { get; set; }
        /// <summary>
        /// 头像url
        /// </summary>
        [StringLength(200)]
        public virtual string Avatar { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        [StringLength(100)]
        public virtual string HiredDate { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        [StringLength(300)]
        public virtual string Roles { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public virtual long? RoleId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 员工在当前开发者企业账号范围内的唯一标识，系统生成，固定值，不会改变
        /// </summary>
        public virtual string Unionid { get; set; }
    }

}

using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("201905241730_GYSWP")]
    partial class GYSWP
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
//            modelBuilder.Entity("GYSWP.Organizations.Organization", b =>
//            {
//                b.Property<long>("Id").ValueGeneratedOnAdd();
//                b.Property<string>("DepartmentName").IsRequired().HasMaxLength(100);
//                b.Property<long?>("ParentId");
//                b.Property<long?>("Order");
//                b.Property<bool?>("DeptHiding");
//                b.Property<string>("OrgDeptOwner").HasMaxLength(100);
//                b.Property<DateTime?>("CreationTime");
//                b.HasKey("Id");

//                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

//                b.ToTable("Organizations");
//            });
//            modelBuilder.Entity("GYSWP.Employees.Employee", b => {
//                b.Property<string>("Id")
//.ValueGeneratedOnAdd(); b.Property<string>("OpenId").HasMaxLength(200); b.Property<string>("Name").HasMaxLength(50); b.Property<string>("Mobile").HasMaxLength(11); b.Property<string>("Email").HasMaxLength(100); b.Property<bool?>("Active"); b.Property<bool?>("IsAdmin"); b.Property<bool?>("IsBoss"); b.Property<string>("Department").HasMaxLength(300); b.Property<string>("Position").HasMaxLength(100); b.Property<string>("Avatar").HasMaxLength(200); b.Property<string>("HiredDate").HasMaxLength(100); b.Property<string>("Roles").HasMaxLength(300); b.Property<long?>("RoleId"); b.Property<string>("Remark").HasMaxLength(500); b.HasKey("Id");

//                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

//                b.ToTable("Employees");
//            });
        }
    }
}

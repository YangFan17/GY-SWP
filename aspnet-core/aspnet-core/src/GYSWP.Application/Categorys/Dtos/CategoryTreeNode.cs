using GYSWP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.Categorys.Dtos
{
    public class CategoryTreeNode : NzTreeNode
    {
        public int? ParentId { get; set; }

        //public override bool expanded
        //{
        //    get
        //    {
        //        if (ParentId == 0)//一级分类
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        public override bool isLeaf
        {
            get
            {
                if (children.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public new List<CategoryTreeNode> children { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.Documents.Dtos
{
    public class DingDocumentEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        /// <summary>
        /// DocNo
        /// </summary>
        [Required(ErrorMessage = "DocNo不能为空")]
        public string DocNo { get; set; }


        /// <summary>
        /// 电子章数组 （以逗号分隔）
        /// </summary>
        [Required(ErrorMessage = "Stamps不能为空")]
        [StringLength(50)]
        public string Stamps { get; set; }
    }
}

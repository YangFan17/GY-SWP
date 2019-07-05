using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.SelfChekRecords.Dtos
{
    public class SelfChekDDInput
    {

        public Guid ClauseId { get; set; }

        public Guid DocId { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}

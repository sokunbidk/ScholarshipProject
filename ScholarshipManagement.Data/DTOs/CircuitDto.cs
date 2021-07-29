using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class CircuitDto : BaseEntity
    {
        public int CircuitName { get; set; }
        

        public IList<JamaatDto> Jamaats { get; set; }
    }
}

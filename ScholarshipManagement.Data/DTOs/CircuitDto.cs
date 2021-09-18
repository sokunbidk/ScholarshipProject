using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class CircuitDto 
    {
        public int Id { get; set; }

        public string CircuitName { get; set; }

        public string Email { get; set; }

        public static implicit operator CircuitDto(JamaatDto v)
        {
            throw new NotImplementedException();
        }

        //public IList<JamaatDto> Jamaats { get; set; }
    }
}

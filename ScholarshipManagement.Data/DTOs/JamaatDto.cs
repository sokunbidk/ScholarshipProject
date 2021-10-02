using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class JamaatDto
    {
        public int Id { get; set; }
        public int Circuitid { get; set; }
        public string CircuitName { get; set; }
        public string JamaatName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}

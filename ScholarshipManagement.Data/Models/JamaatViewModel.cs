using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data
{
    public class JamaatViewModel
    {
        public int Id { get; set; }
        public int Circuitid { get; set; }
        public string CircuitName { get; set; }
        public Circuit Circuit { get; set; } //navigation properties
        public string Name { get; set; }
        

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
    }


    public class CreateJamaatRequestModel
    {
        public int CircuitId { get; set; }

        public Circuit Circuit { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
    public class UpdateJamaatRequestModel
    {
        public int Circuitid { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
    public class JamaatsResponseModel : BaseResponse
    {
        public IEnumerable<JamaatDto> Data { get; set; } = new List<JamaatDto>();
    }

    public class JamaatResponseModel : BaseResponse
    {
        public JamaatDto Data { get; set; }
    }
}

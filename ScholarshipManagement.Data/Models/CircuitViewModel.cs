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
    public class CircuitViewModel
    {
        public int Id { get; set; }
        public string CircuitName { get; set; }

        public string Email { get; set; }
        public int PresidentId { get; set; }
    }


    public class CreateCircuitRequestModel
    {
        public string CircuitName { get; set; }

        public string Email { get; set; }
        public int PresidentId { get; set; }


    }
    public class UpdateCircuitRequestModel
    {
        public string CircuitName { get; set; }

        public string Email { get; set; }
    }
    public class CircuitsResponseModel : BaseResponse
    {
        public IEnumerable<CircuitDto> Data { get; set; } = new List<CircuitDto>();
    }

    public class CircuitResponseModel : BaseResponse
    {
        public CircuitDto Data { get; set; }
    }
}

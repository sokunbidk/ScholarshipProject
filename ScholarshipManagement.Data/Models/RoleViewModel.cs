using ScholarshipManagement.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data
{
    public class RoleViewModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
       
    }

    public class CreateRoleRequestModel
    {
      
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
    }
    public class UpdateRoleRequestModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RoleName")]
        public string RoleName { get; set; }    
    }

    public class RolesResponseModel : BaseResponse
    {
        public IEnumerable<RoleDto> Data { get; set; } = new List<RoleDto>();
    }

    public class RoleResponseModel : BaseResponse
    {
        public RoleDto Data { get; set; }
    }
}

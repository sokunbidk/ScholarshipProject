using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }

        public Student Student { get; set; }

        public string MemberCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
